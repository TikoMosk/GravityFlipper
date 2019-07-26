using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static MovementController _controller;
    public static MovementController Controller { get { return _controller; } }

    Node previousClickedNode;
    Node.Direction previousDirection;

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
        Vector3 forwardDirection = Vector3.zero;

        if (GameController.Game.levelController.Level.GetNodeDistance(playerNode,destinationNode) <= 1)
        {
            if(playerNode.HasSamePosition(destinationNode) && !previousClickedNode.HasSamePosition(n) && GameController.Game.levelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
            {
                forwardDirection = Level.GetVectorByDirection(previousDirection);
                Quaternion rotation = Quaternion.LookRotation(forwardDirection, Level.GetVectorByDirection(direction));
                playerNode.MoveableObject.MoveableGameObject.transform.rotation = rotation;
                //Debug.Log(forwardDirection + " PREVIOUSDIRECTION");
            }
            else if(!playerNode.HasSamePosition(destinationNode))
            {
                forwardDirection = destinationNode.GetPosition() - playerNode.GetPosition();
                Quaternion rotation = Quaternion.LookRotation(forwardDirection, Level.GetVectorByDirection(direction));
                playerNode.MoveableObject.MoveableGameObject.transform.rotation = rotation;
            }


            if(previousClickedNode != null && GameController.Game.levelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
            {
                
                GameController.Game.levelController.Level.MovePlayer(destinationNode);
                previousClickedNode = n;
            }
            else if(previousClickedNode == null)
            {
                GameController.Game.levelController.Level.MovePlayer(destinationNode);
                previousClickedNode = n;
            }
            
        }
        else
        {
            return;
        }
        previousDirection = direction;
        //Debug.Log(LevelController.Instance.Level.GetNodeDistance(playerNode, destinationNode));

    }
}
