using System.Collections;
using System.Collections.Generic;
using VolumetricLines;
using UnityEngine;

public class LaserRay : MonoBehaviour
{
    public Vector3 startPos;
    private Vector3 endPos;
    private Node currentNode;

    private void Start()
    {
        currentNode = GameController.Game.CurrentLevel.GetNode();

        GetComponent<VolumetricLineBehavior>().StartPos = this.startPos;
        GetComponent<VolumetricLineBehavior>().EndPos = this.endPos;
    }

    private bool Check()
    {
        if (true)
        {

        }
    }
}
