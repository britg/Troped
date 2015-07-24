using UnityEngine;
using System.Collections;

public class BaseBehaviour : MonoBehaviour {

	private Game _game;
	public Game game {
		get {
			if (_game == null) {
				_game = GameObject.Find("Game").GetComponent<GameBinding>().game;
			}
			return _game;
		}
	}

	private TurnManager _turnManager;
	public TurnManager turnManager {
		get {
			if (_turnManager == null) {
				_turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
			}
			return _turnManager;
		}
	}

	protected void QueueAction (GameAction action) {
		game.actionQueue.Add(action);
	}

	protected void TriggerActionFinished () {
		game.actionQueue.Finish();
	}
}
