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
        Node destinationNode = GameController.Game.currentLevel.GetNodeInTheDirection(n, direction);
        //Debug.Log(destinationNode.GetNodePosition());
        playerNode = GameController.Game.levelController.Level.GetPlayerNode();
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
                Debug.Log("1");
                forwardDirection = GameController.Game.levelController.transform.TransformVector( Level.GetVectorByDirection(previousDirection));
                upDirection = GameController.Game.levelController.transform.TransformVector(Level.GetVectorByDirection(direction));
                Debug.Log("FORWARD DIRECTION IS " + forwardDirection + " UP DIRECTION IS " + upDirection);
                rotation = Quaternion.LookRotation(forwardDirection, upDirection);
                playerNode.MoveableObject.MoveableGameObject.transform.rotation = rotation;
                Quaternion worldRotation = Quaternion.LookRotation(Vector3.Cross(upDirection,Vector3.right), forwardDirection);
                //GameController.Game.levelController.transform.rotation = worldRotation;
                GameController.Game.levelController.transform.position = Vector3.zero;
            }
            else if(!playerNode.HasSamePosition(destinationNode) && GameController.Game.levelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
            {
                Debug.Log("2");
                forwardDirection = destinationNode.GetPosition() - playerNode.GetPosition();
                upDirection = GameController.Game.levelController.transform.TransformVector(Level.GetVectorByDirection(direction));
                rotation = Quaternion.LookRotation(forwardDirection, upDirection);
                playerNode.MoveableObject.MoveableGameObject.transform.rotation = rotation;
               // GameController.Game.levelController.transform.rotation = Quaternion.LookRotation(GameController.Game.levelController.transform.forward, upDirection);
            }


            if (previousClickedNode != null && GameController.Game.currentLevel.GetNodeDistance(previousClickedNode, n) < 3)
            {
                GameController.Game.currentLevel.MovePlayer(destinationNode);
                previousClickedNode = n;
                previousDirection = direction;
            }
            else if(previousClickedNode == null)
            {
                GameController.Game.currentLevel.MovePlayer(destinationNode);
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
