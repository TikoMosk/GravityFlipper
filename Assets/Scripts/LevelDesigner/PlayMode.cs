using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : GameState
{
    public PlayMode() {
        Time.timeScale = 1f;
        GameController.Game.LevelController.BuildTestLevel();
    }
    public void OnNodeClick(Node n, Node.Direction dir)
    {
        GameController.Game.CurrentLevel.Player.Move(n,dir);
    }
}
