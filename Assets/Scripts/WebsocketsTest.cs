using UnityEngine;
using System.Collections;
using WebSocketSharp;

public class WebsocketsTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartWebsockets();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartWebsockets () {
		using (var ws = new WebSocket ("ws://dragonsnest.far/Laputa")) {
			Debug.Log ("Starting websockets");
			ws.OnMessage += (sender, e) =>
				Debug.Log ("Laputa says: " + e.Data);

			ws.OnOpen += (sender, e) =>
				Debug.Log ("On open " + e);

			ws.OnError += (sender, e) => 
				Debug.Log ("On error " + e.Exception);
			
			ws.Connect ();
			ws.Send ("BALUS");
		}
	}
}
