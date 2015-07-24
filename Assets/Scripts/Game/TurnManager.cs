using UnityEngine;
using System.Collections;

public class TurnManager : BaseBehaviour {

	// Use this for initialization
	void Start () {
		QueueAction(new GameAction(this, "ChangeTurn"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void EndTurn () {
		//TODO: Announce the turn is ending so cleanup can be done.
		ChangeTurn();
	}

	void ChangeTurn () {
		game.NextTurn();
		Debug.Log ("turn is " + game.turn);
		NotificationCenter.PostNotification(Constants.OnTurnChange, 
		                                    iTween.Hash("turn", game.turn));
		TriggerActionFinished();
	}
}
