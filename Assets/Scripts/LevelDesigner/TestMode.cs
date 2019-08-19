using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMode : GameState
{
    public TestMode() {
        Time.timeScale = 1f;
        //GameController.Game.CameraController.ResetCamera();
    }
    public void OnNodeClick(Node n, Node.Direction dir) {
        GameController.Game.CurrentLevel.Player.Move(n, dir);
    }
    public void Update() {
        GameController.Game.CameraController.CameraPositionPlayMode();
    }
    public void SetInputs(Vector3 camMovementVec) {
    }
}
