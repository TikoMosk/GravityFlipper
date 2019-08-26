using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeGraphic : MonoBehaviour
{
    private Action<Node.Direction> onClick;
    private Node node;

    public Node Node { get => node; set => node = value; }

    public void GetClicked(Node.Direction dir)
    {
        onClick?.Invoke(dir);
    }
    public void RegisterToClick(Action<Node.Direction> onClick) {
        this.onClick += onClick;
    }
}
