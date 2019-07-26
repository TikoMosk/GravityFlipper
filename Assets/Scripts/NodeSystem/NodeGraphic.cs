using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeGraphic : MonoBehaviour
{
    public event Action<Node.Direction> onClick;

    public void GetClicked(Node.Direction dir)
    {
        onClick?.Invoke(dir);
    }
    public void RotateGraphicToDirection(Node.Direction dir)
    {

    }
}
