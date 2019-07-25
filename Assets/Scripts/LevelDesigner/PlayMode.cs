using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : GameMode
{
    public void OnNodeClick(Node n)
    {
        GameController.Game.movementController.OnClick(n);
    }
}
