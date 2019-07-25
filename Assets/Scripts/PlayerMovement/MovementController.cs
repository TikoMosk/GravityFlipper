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
    public void OnClick(Node n, Node.Direction direction)
    {

        Node destinationNode = GameController.Game.levelController.Level.GetNodeInTheDirection(n, direction);
        playerNode = GameController.Game.levelController.Level.GetPlayerNode();
        if (GameController.Game.levelController.Level.GetNodeDistance(playerNode,destinationNode) == 1)
        {
            GameController.Game.levelController.Level.MovePlayer(destinationNode);
        }

        else
        {
            return;
        }
        //Debug.Log(LevelController.Instance.Level.GetNodeDistance(playerNode, destinationNode));

    }
}
