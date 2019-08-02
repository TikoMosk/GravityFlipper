using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static MovementController _controller;
    public static MovementController Controller { get { return _controller; } }
    public GameObject cameraObject;

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
    private void Update()
    {
        
    }
    public void OnClick(Node n, Node.Direction direction)
    {
        if (!GameController.Game.SmoothGraphics.isMovingSmoothly)
        {
            Node destinationNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, direction);
            playerNode = GameController.Game.LevelController.PlayerNode;

            Vector3 forwardDirection = Vector3.zero;
            Vector3 upDirection = Vector3.zero;
            Quaternion rotation;

            if (GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1)
            {
                if (previousClickedNode == null)
                {

                    previousClickedNode = n;
                }
                if (playerNode.HasSamePosition(destinationNode) && !previousClickedNode.HasSamePosition(n) && GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
                {
                    forwardDirection = GameController.Game.LevelController.transform.TransformVector(Level.GetVectorByDirection(previousDirection));
                    upDirection = GameController.Game.LevelController.transform.TransformVector(Level.GetVectorByDirection(direction));
                    rotation = Quaternion.LookRotation(forwardDirection, upDirection);
                    playerNode.NodeMember.NodeObjectGraphic.transform.rotation = rotation;
                    GameController.Game.SmoothGraphics.RotateWorld(Vector3.Cross(forwardDirection, upDirection));

                }
                else if (!playerNode.HasSamePosition(destinationNode) && GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
                {
                    forwardDirection = destinationNode.GetPosition() - playerNode.GetPosition();
                    upDirection = GameController.Game.LevelController.transform.TransformVector(Level.GetVectorByDirection(direction));
                    rotation = Quaternion.LookRotation(forwardDirection, upDirection);
                    playerNode.NodeMember.NodeObjectGraphic.transform.rotation = rotation;
                }


                if (previousClickedNode != null && GameController.Game.CurrentLevel.GetNodeDistance(previousClickedNode, n) < 3)
                {
                    GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
                    previousClickedNode = n;
                    previousDirection = direction;
                }
                else if (previousClickedNode == null)
                {
                    GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
                    previousClickedNode = n;
                }

            }
            else
            {
                return;
            }

        }
    }
        
}
