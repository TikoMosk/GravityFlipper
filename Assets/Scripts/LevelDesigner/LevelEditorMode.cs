using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorMode : GameMode
{
    public void OnNodeClick(Node n, Node.Direction dir, int button)
    {
        GameController.Game.LevelDesignController.OnNodeClick(n,dir, button);
    }
}
