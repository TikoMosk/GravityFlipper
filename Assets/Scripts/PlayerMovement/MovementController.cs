using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static MovementController _controller;
    public static MovementController Controller { get { return _controller; } }

    Node playerNode;
    private void Awake()
    {
        if(_controller != null & _controller != this)
        {
            Destroy(this);
        }
        else
        {
            _controller = this;
        }
    }
    public void OnClick(Node n)
    {
        Node destinationNode = LevelController.Instance.Level.GetNode(n.X, n.Y + 1, n.Z);
        playerNode = LevelController.Instance.GetPlayerNode();
        LevelController.Instance.Level.MovePlayer(destinationNode);
    }
}
