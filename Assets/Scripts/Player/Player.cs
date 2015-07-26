using UnityEngine;
using System.Collections;

public class Player {

	public int movesPerTurn = 1;
	public int movesThisTurn = 0;
	public int totalActionPoints = 10;
	public int usedActionPoints = 10;


  public bool canMove {
    get {
      return movesThisTurn < movesPerTurn;
    }
  }

  public void NewTurn () {
    movesThisTurn = 0;
  }
}
