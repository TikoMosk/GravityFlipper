using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static MovementController _controller;
    public static MovementController Controller { get { return _controller; } }
    public GravityView gravityView;
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
        if(playerNode != null)
        {
            playerNode = GameController.Game.levelController.PlayerNode;
            cameraObject.transform.position = playerNode.NodeObject.NodeObjectGraphic.transform.position;
        }
    }
    public void OnClick(Node n, Node.Direction direction)
    {
        Node destinationNode = GameController.Game.currentLevel.GetNodeInTheDirection(n, direction);
        playerNode = GameController.Game.levelController.PlayerNode;
        Debug.Log(playerNode.NodeGraphic);
        
        Vector3 forwardDirection = Vector3.zero;
        Vector3 upDirection = Vector3.zero;
        Quaternion rotation;

        if (GameController.Game.levelController.Level.GetNodeDistance(playerNode,destinationNode) <= 1)
        {
            if(previousClickedNode == null)
            {

                previousClickedNode = n;
            }
            if(playerNode.HasSamePosition(destinationNode) && !previousClickedNode.HasSamePosition(n) && GameController.Game.levelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
            {
                forwardDirection = GameController.Game.levelController.transform.TransformVector( Level.GetVectorByDirection(previousDirection));
                upDirection = GameController.Game.levelController.transform.TransformVector(Level.GetVectorByDirection(direction));
                rotation = Quaternion.LookRotation(forwardDirection, upDirection);
                playerNode.NodeObject.NodeObjectGraphic.transform.rotation = rotation;
                Quaternion worldRotation = Quaternion.LookRotation(Vector3.Cross(upDirection,Vector3.right), forwardDirection);
                gravityView.RotateWorld(Vector3.Cross(forwardDirection, upDirection));
                GameController.Game.levelController.transform.position = Vector3.zero;
            }
            else if(!playerNode.HasSamePosition(destinationNode) && GameController.Game.levelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
            {
                forwardDirection = destinationNode.GetPosition() - playerNode.GetPosition();
                upDirection = GameController.Game.levelController.transform.TransformVector(Level.GetVectorByDirection(direction));
                rotation = Quaternion.LookRotation(forwardDirection, upDirection);
                playerNode.NodeObject.NodeObjectGraphic.transform.rotation = rotation;
                
                
            }


            if (previousClickedNode != null && GameController.Game.currentLevel.GetNodeDistance(previousClickedNode, n) < 3)
            {
                GameController.Game.currentLevel.MoveObject(playerNode,destinationNode);
                previousClickedNode = n;
                previousDirection = direction;
            }
            else if(previousClickedNode == null)
            {
                GameController.Game.currentLevel.MoveObject(playerNode,destinationNode);
                previousClickedNode = n;
            }
            
        }
        else
        {
            return;
        }
      

    }
}
