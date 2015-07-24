using UnityEngine;
using System.Collections;

public class GameAction {

	public MonoBehaviour behaviour;
	public string methodName;

	public GameAction (MonoBehaviour _behaviour, string _methodName) {
		behaviour = _behaviour;
		methodName = _methodName;
	}

	public override string ToString () {
		return string.Format ("[GameAction] {0} {1}", behaviour, methodName);
	}

}
