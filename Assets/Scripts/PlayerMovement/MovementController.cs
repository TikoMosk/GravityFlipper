using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static MovementController _controller;
    public static MovementController Controller { get { return _controller; } }

    Node previousClickedNode;

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
        //Debug.Log(n.GetNodePosition());
        Node destinationNode = GameController.Game.levelController.Level.GetNodeInTheDirection(n, direction);
        //Debug.Log(destinationNode.GetNodePosition());
        playerNode = GameController.Game.levelController.Level.GetPlayerNode();
        if (GameController.Game.levelController.Level.GetNodeDistance(playerNode,destinationNode) <= 1 )
        {
            if(previousClickedNode != null && GameController.Game.levelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
            {
                Debug.Log(Level.GetVectorByDirection(direction));
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Level.GetVectorByDirection(direction));
                playerNode.MoveableObject.MoveableGameObject.transform.rotation = rotation;
                GameController.Game.levelController.Level.MovePlayer(destinationNode);
                previousClickedNode = n;
            }
            else if(previousClickedNode == null)
            {
                Debug.Log(Level.GetVectorByDirection(direction));
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Level.GetVectorByDirection(direction));
                playerNode.MoveableObject.MoveableGameObject.transform.rotation = rotation;
                GameController.Game.levelController.Level.MovePlayer(destinationNode);
                previousClickedNode = n;
            }
            
        }
        else
        {
            return;
        }
        //Debug.Log(LevelController.Instance.Level.GetNodeDistance(playerNode, destinationNode));

    }
}
