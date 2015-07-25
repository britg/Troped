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

  private ActionQueue _actionQueue;
  public ActionQueue actionQueue {
    get {
      if (_actionQueue == null) {
        _actionQueue = GameObject.Find("ActionQueue").GetComponent<ActionQueue>();
      }

      return _actionQueue;
    }
  }

	protected void QueueAction (GameAction action) {
		actionQueue.Add(action);
	}

	protected void TriggerActionFinished () {
		actionQueue.Finish();
	}
}
