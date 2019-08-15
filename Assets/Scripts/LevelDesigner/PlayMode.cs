using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : GameState
{
    public PlayMode() {
        Time.timeScale = 1f;
        
    }
    public void SetInputs(Vector3 camMovementVec) {
    }
    public void OnNodeClick(Node n, Node.Direction dir)
    {
        GameController.Game.CurrentLevel.Player.Move(n,dir);
    }
    public void Update() {
        GameController.Game.CameraController.CameraPositionPlayMode();
    }
}
