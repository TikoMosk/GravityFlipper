using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorMode : GameState
{
    public void OnStart() {

    }
    public void OnNodeClick(Node n, Node.Direction dir)
    {
        GameController.Game.LevelDesignController.OnNodeClick(n,dir);
    }
}
