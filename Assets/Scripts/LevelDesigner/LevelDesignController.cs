using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesignController : MonoBehaviour
{
    public GameObject block;

    public void OnNodeClick(Node n, Node.Direction dir)
    {
        Node placeNode = GameController.Game.levelController.Level.GetNodeInTheDirection(n, dir);
        GameController.Game.levelController.Level.SetNode(placeNode.X,placeNode.Y,placeNode.Z, 1);
    }
}
