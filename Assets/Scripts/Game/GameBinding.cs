using UnityEngine;
using System.Collections;

public class GameBinding : BaseBehaviour {

  public new Game game;

  public GameObject hexPrefab;
  public GameObject playerPrefab;

	void Awake () {
		game = new Game();
    game.binding = this;
		game.Init();

    CreateLevel();
	}

  void CreateLevel () {

    foreach (Vector3 tilePos in game.level.tiles) {
      var hex = (GameObject)Instantiate(hexPrefab);
      hex.transform.position = tilePos;
    }

    var playerObj = (GameObject)Instantiate(playerPrefab);
    playerObj.transform.position = game.level.playerTile;
    playerObj.name = "Player";
  }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

  public void QueueFinishEnemyTurns () {
    turnManager.QueueEndTurn();
  }

}
