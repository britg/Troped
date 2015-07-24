using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseGridMove : MonoBehaviour {

	public bool canMove = true;

	float distBetweenTiles = 0f;
	public float tileDistanceFudgeFactor = 0.1f;

	// Use this for initialization
	void Start () {
		CalcDistanceBetweenTiles();
	}
	
	// Update is called once per frame
	void Update () {
		DetectClick();
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

	void QueueMoveToTile (GameObject tile) {
		if (!ValidMove(tile)) {
			return;
		}

//		transform.position = tile.transform.position;
		iTween.MoveTo (gameObject, iTween.Hash ("position", tile.transform.position));

	}

	bool ValidMove (GameObject tile) {
		return AdjacentToPlayer(tile);

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
}
