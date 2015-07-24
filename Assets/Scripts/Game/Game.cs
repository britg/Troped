using UnityEngine;
using System.Collections;

public class Game {

	public Player player;
	public Turn turn;
	public ActionQueue actionQueue;

	public void Init () {
		player = new Player();
		turn = Turn.None;
		actionQueue = new ActionQueue();
	}

	public void Tick () {
		actionQueue.Tick();
	}

	public void NextTurn () {
		if (turn == Turn.Player) {
			turn = Turn.Enemies;
		} else {
			turn = Turn.Player;
		}
	}

}
