using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level {

  public int floorNum;
  public Wing wing;

  int radius = 10;
  int tileSize = 4;
  public List<Vector3> tiles;
  public Vector3 playerTile;

  public List<Enemy> Enemies () {
    var enemies = new List<Enemy>();

    return enemies;
  }

  public void Init () {
    CreateTiles();
    PlacePlayerRandomly();
    // Place the entrance, set the player there
    // place the exit
    // Place obstacles
    // determine enemies and place them
  }

  void CreateTiles () {
    tiles = new List<Vector3>();
    tiles.Add(Vector3.zero);


    for (int row = -radius; row < radius; row++) {
      var start = Vector3.forward * tileSize * row;
      int additional = row + radius;
      int shift = additional;
      if (row >= 0) {
        additional = radius - row;
        shift = additional + row;
      }
      for (int i = 0; i < radius + additional; i++) {
        var newStart = start + (Quaternion.Euler(0, 240f, 0) * Vector3.forward * tileSize) * shift;
        Vector3 next = newStart + (Quaternion.Euler(0, 60f, 0) * Vector3.forward * i * tileSize);
        tiles.Add(next);
      }
    }
  }

  void PlacePlayerRandomly () {
    int rand = Random.Range(0, tiles.Count-1);
    playerTile = tiles[rand];
  }

  /*
    Hex ratio of up to diag: 1.1094;
  */

}
