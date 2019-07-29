using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : GameMode
{
    public void OnNodeClick(Node n, Node.Direction dir, int button)
    {
        GameController.Game.movementController.OnClick(n, dir);
    }
}
