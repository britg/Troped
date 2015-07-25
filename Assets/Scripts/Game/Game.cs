using UnityEngine;
using System.Collections;

public class Game {

	public Player player;
	public Turn turn;

	public void Init () {
		player = new Player();
		turn = Turn.None;
	}

	public void Tick () {
	}

	public void NextTurn () {
		if (turn == Turn.Player) {
			turn = Turn.Enemies;
		} else {
			turn = Turn.Player;
		}
	}

}
