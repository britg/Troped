using UnityEngine;
using System.Collections;

public class Player {

  public int movesPerTurn = 1;
  public int movesThisTurn = 0;
  public int totalActionPoints = 10;
  public int usedActionPoints = 10;

  float maxMoveDistance = 5f; // magic number

  public Vector3 pos;


  public bool canMove {
    get {
      return movesThisTurn < movesPerTurn;
    }
  }

  public void NewTurn () {
    movesThisTurn = 0;
  }

  public bool ValidMove (Vector3 tile) {
    if (!canMove) {
      return false;
    }

    float dist = Vector3.Distance(pos, tile);
    if (dist > maxMoveDistance) {
      Debug.Log("Distance too far!");
      return false;
    }

    if (Mathf.Approximately(dist, 0f)) {
      Debug.Log("Same tile");
      return false;
    }

    return true;
  }
}
