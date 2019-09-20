using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState {

    void OnNodeClick(Node n, Node.Direction dir);

    void Update();
    void SetInputs(Vector3 camMovementVec);
}
