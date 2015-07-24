using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseGridMove : BaseBehaviour {

	public bool canMove = false;

	int movesThisTurn = 0;

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

			if (obj.tag == "Tile") {
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
		movesThisTurn++;
		iTween.MoveTo (gameObject, 
		               iTween.Hash ("position", queuedPosition,
		             			    "oncomplete", "TriggerActionFinished"));
	}

	bool ValidMove (GameObject tile) {
		return (movesThisTurn < game.player.movesPerTurn) 
				&& AdjacentToPlayer(tile);

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
			movesThisTurn = 0;
			canMove = true;
		} else {
			canMove = false;
		}
	}
}
