using UnityEngine;
using System.Collections;

public class RotationController : BaseBehaviour {

	// Use this for initialization
	void Start () {
    ApplyRotation();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void ApplyRotation () {
    var angles = transform.eulerAngles;
    angles.y = game.player.rotation;
    transform.eulerAngles = angles;
  }
}
