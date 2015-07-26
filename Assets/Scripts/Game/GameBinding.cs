using UnityEngine;
using System.Collections;

public class GameBinding : BaseBehaviour {

  public new Game game;

	void Awake () {
		game = new Game();
    game.binding = this;
		game.Init();
	}

	// Use this for initialization
	void Start () {
    var center = GameObject.Find("(0, 0)");
    var diag = GameObject.Find("(1, 0)");
    var up = GameObject.Find("(0, 1)");

    var diagdist = Vector3.Distance(center.transform.position, diag.transform.position);
    var updist = Vector3.Distance(center.transform.position, up.transform.position);


	}
	
	// Update is called once per frame
	void Update () {
	}

  public void QueueFinishEnemyTurns () {
    turnManager.QueueEndTurn();
  }

}
