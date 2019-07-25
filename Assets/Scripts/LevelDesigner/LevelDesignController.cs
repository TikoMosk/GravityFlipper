using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesignController : MonoBehaviour
{
    public GameObject block;

    public void OnNodeClick(Node n)
    {
        GameController.Game.levelController.Level.SetNode(n.X, n.Y + 1, n.Z, 1);
    }
}
