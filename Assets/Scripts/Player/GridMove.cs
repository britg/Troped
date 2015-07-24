using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GridMove : MonoBehaviour {

	public bool canMove = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DetectClick();
	}

	void DetectClick () {

		if (!Input.GetMouseButtonDown(0)) {
			return;
		}

		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction*1000, Color.yellow);

		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit, 1000f)) {
			Debug.Log ("Hit something");
			var obj = hit.collider.gameObject;
		}

	}
}
