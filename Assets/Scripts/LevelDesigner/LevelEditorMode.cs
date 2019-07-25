using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorMode : GameMode
{
    public void OnNodeClick(Node n)
    {
        GameController.Game.levelDesignController.OnNodeClick(n);
    }
}
