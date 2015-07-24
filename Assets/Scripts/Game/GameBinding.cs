using UnityEngine;
using System.Collections;

public class GameBinding : MonoBehaviour {

	public Game game;

	void Awake () {
		game = new Game();
		game.Init();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		game.Tick();
	}
}
