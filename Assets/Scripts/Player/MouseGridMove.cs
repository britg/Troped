using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseGridMove : BaseBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (game.player.canMove) {
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
		if (!game.player.ValidMove(tile.transform.position)) {
			return;
		}

		queuedPosition = tile.transform.position;
		QueueAction(new GameAction(this, "MoveToQueuedPosition"));
	}

	void MoveToQueuedPosition () {
		game.player.movesThisTurn++;
    game.player.pos = queuedPosition;
    iTween.MoveTo(gameObject,
                   iTween.Hash("position", game.player.pos,
                         "easetype", iTween.EaseType.linear,
                         "time", 0.2f,
                           "oncomplete", "TriggerActionFinished"));
  }
}

