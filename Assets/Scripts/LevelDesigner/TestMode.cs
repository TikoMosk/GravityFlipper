using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMode : GameState
{
   
    public void OnNodeClick(Node n, Node.Direction dir) {
        GameController.Game.CurrentLevel.Player.Move(n, dir);
    }
}
