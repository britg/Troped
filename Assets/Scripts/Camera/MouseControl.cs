using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseControl : MonoBehaviour {

	public bool playerAnchor = false;
	public Transform target;
	public float distance = 10f;
	public float xSpeed = 0f;
	public float ySpeed = 0f;
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float orbitMultiplier = 0.02f;
	public float zoomMultiplier = 1f;

	// Move
	Vector3 lastMousePosition;
	public float moveMultiplier = 0.1f;

	float x = 0f;
	float y = 0f;
//	float z = 0f;

	// Use this for initialization
	void Start () {
		Init();
	}

	void Init () {
		var _ang = transform.eulerAngles;
		x = _ang.y;
		y = _ang.x;
		distance = Vector3.Distance(target.position, transform.position);

		lastMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (playerAnchor) {
			DetectOrbit();
		}
		DetectZoom();
		DetectMove();
	}

	void DetectOrbit () {
		if (target == null) {
			return;
		}

		if (Input.GetMouseButton(1)) {
			DoOrbit();
		}
	}

	void DoOrbit () {
		x += Input.GetAxis("Mouse X") * xSpeed * orbitMultiplier;
		y -= Input.GetAxis("Mouse Y") * ySpeed * orbitMultiplier;

		y = ClampAngle(y, yMinLimit, yMaxLimit);
		SetTransform();
	}

	void SetTransform () {
		var rotation = Quaternion.Euler(y, x, 0);
		var position = rotation * new Vector3(0f, 0f, -distance) + target.position;
		transform.rotation = rotation;
		transform.position = position;
	}

	void DetectZoom () {
		var mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
		var change = zoomMultiplier * mouseWheelInput;

		if (!Mathf.Approximately(change, 0f)) {
			var pos = Camera.main.transform.position;
			pos.y += zoomMultiplier * mouseWheelInput;
			Camera.main.transform.position = pos;
		}
	}

	void DetectMove () {

		if (Input.GetMouseButtonDown(0)) {
			lastMousePosition = Input.mousePosition;
		}

		if (!Input.GetMouseButton(0)) {
			return;
		}

		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		var camPos = Camera.main.transform.position;
		var delta = lastMousePosition - Input.mousePosition;
		lastMousePosition = Input.mousePosition;
		camPos.x += delta.x * moveMultiplier;
		camPos.z += delta.y * moveMultiplier;

		Camera.main.transform.position = camPos;
	}

	float ClampAngle (float angle, float min, float max) {
		if (angle < -360) {
			angle += 360;
		}

		if (angle > 360) {
			angle -= 360;
		}

		return Mathf.Clamp(angle, min, max);
	}

}

//
//var target : Transform;
//var distance = 10.0;
//
//var xSpeed = 250.0;
//var ySpeed = 120.0;
//
//var yMinLimit = -20;
//var yMaxLimit = 80;
//
//private var x = 0.0;
//private var y = 0.0;
//private var z = 0.0;
//
//@script AddComponentMenu("Camera-Control/Mouse Orbit")
//	
//function Start () {
//	var angles = transform.eulerAngles;
//	x = angles.y;
//	y = angles.x;
//	distance = Vector3.Distance(target.position, transform.position);
//	
//	// Make the rigid body not change rotation
//	if (GetComponent.<Rigidbody>())
//		GetComponent.<Rigidbody>().freezeRotation = true;
//}
//
//function LateUpdate () {
//	if (target && Input.GetMouseButton(1)) {
//		x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
//		y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
//		
//		y = ClampAngle(y, yMinLimit, yMaxLimit);
//		
//		var rotation = Quaternion.Euler(y, x, 0);
//		var position = rotation * Vector3(0.0, 0.0, -distance) + target.position;
//		
//		transform.rotation = rotation;
//		transform.position = position;
//	}
//	
//}
//
//static function ClampAngle (angle : float, min : float, max : float) {
//	if (angle < -360)
//		angle += 360;
//	if (angle > 360)
//		angle -= 360;
//	return Mathf.Clamp (angle, min, max);
//}