using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseGridMove : BaseBehaviour {

	public bool canMove = false;

	float distBetweenTiles = 0f;
	public float tileDistanceFudgeFactor = 0.1f;

	// Use this for initialization
	void Start () {
		CalcDistanceBetweenTiles();
		NotificationCenter.AddObserver(this, Constants.OnTurnChange);
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			DetectClick();
		}
	}

	void DetectClick () {

		if (!Input.GetMouseButtonUp(0)) {
			return;
		}

		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hit = new RaycastHit();

		if (Physics.Raycast(ray, out hit, 1000)) {
			var obj = hit.collider.gameObject;

			if (obj.tag == Constants.TileLayer) {
				QueueMoveToTile(obj);
			}
		}
	}

	Vector3 queuedPosition;
	void QueueMoveToTile (GameObject tile) {
		if (!ValidMove(tile)) {
			return;
		}

		queuedPosition = tile.transform.position;
		QueueAction(new GameAction(this, "MoveToQueuedPosition"));
	}

	void MoveToQueuedPosition () {
		game.player.movesThisTurn++;
		iTween.MoveTo (gameObject, 
		               iTween.Hash ("position", queuedPosition,
		             				"easetype", iTween.EaseType.linear,
		             				"time", 0.2f,
		             			    "oncomplete", "TriggerActionFinished"));
	}

	bool ValidMove (GameObject tile) {
    bool enoughMoves = game.player.movesThisTurn < game.player.movesPerTurn;
    if (!enoughMoves) {
      return false;
    }

    bool adjacent = AdjacentToPlayer(tile);
    if (!adjacent) {
      return false;
    }


    bool same = Vector3.Equals(tile.transform.position, transform.position);
    if (same) {
      return false;
    }

    return true;

	}

	bool AdjacentToPlayer(GameObject tile) {
		float tileDist = Vector3.Distance(transform.position, tile.transform.position);
		return  tileDist <= distBetweenTiles;
	}

	void CalcDistanceBetweenTiles () {
		var first = GameObject.Find ("(0, -9)");
		var second = GameObject.Find ("(0, -8)");

		distBetweenTiles = Vector3.Distance(first.transform.position, second.transform.position);
		distBetweenTiles += tileDistanceFudgeFactor;
	}

	void OnTurnChange (Notification n) {
		Turn turn = (Turn)n.data["turn"];
		if (turn == Turn.Player) {
			game.player.movesThisTurn = 0;
			canMove = true;
		} else {
			canMove = false;
		}
	}
}
