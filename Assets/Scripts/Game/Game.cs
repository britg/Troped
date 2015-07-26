﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game {

  public GameBinding binding;
	public Player player;
	public Turn turn;

  public List<Enemy> enemies;

	public void Init () {
		player = new Player();
		turn = Turn.None;
    enemies = new List<Enemy>();
	}

	public void NextTurn () {
		if (turn == Turn.Player) {
			turn = Turn.Enemies;
      TakeEnemyTurns();
		} else {
			turn = Turn.Player;
      player.NewTurn();
		}
	}

  void TakeEnemyTurns () {
    foreach (Enemy enemy in enemies) {
      enemy.TakeTurn();
    }
    FinishEnemyTurns();
  }

  void FinishEnemyTurns () {
    binding.QueueFinishEnemyTurns();
  }

}
