using UnityEngine;
using System.Collections;

public class ActionQueue {

	Queue queue = new Queue();
	GameAction currentAction;

	public void Add (GameAction action) {
		queue.Enqueue(action);
	}

	public void Tick () {
		if (currentAction == null) {
			Next();
		}
	}

	public void Finish () {
		Debug.Log ("Finished with action " + currentAction);
		currentAction = null;
	}

	void Next () {
		if (queue.Count < 1) {
			return;
		}
		currentAction = (GameAction)queue.Dequeue();
		currentAction.behaviour.SendMessage(currentAction.methodName, 
		                                    SendMessageOptions.RequireReceiver);
	}

}
