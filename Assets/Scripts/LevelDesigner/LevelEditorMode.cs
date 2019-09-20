using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorMode : IGameState
{
    Vector3 inputVector;

    public void OnNodeClick(Node n, Node.Direction dir)
    {
        GameController.Game.LevelDesignController.OnNodeClick(n,dir);
    }
    public void Update() {
        GameController.Game.CameraController.CameraPositionLEMode(inputVector);
    }
    public void SetInputs(Vector3 camMovementVec) {
        inputVector = camMovementVec;
    }
}
