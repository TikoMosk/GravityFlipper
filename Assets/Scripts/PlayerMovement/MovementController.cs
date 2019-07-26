using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static MovementController _controller;
    public static MovementController Controller { get { return _controller; } }

<<<<<<< HEAD
    Node previousClickedNode;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
    Node.Direction previousDirection;
>>>>>>> nodeSystem
=======
    Node.Direction previousDirection;
>>>>>>> nodeSystem
=======
    Node.Direction previousDirection;
>>>>>>> nodeSystem

=======
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System
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
<<<<<<< HEAD
        //Debug.Log(n.GetNodePosition());
        Node destinationNode = GameController.Game.levelController.Level.GetNodeInTheDirection(n, direction);
        //Debug.Log(destinationNode.GetNodePosition());
        playerNode = GameController.Game.levelController.Level.GetPlayerNode();
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        if (GameController.Game.levelController.Level.GetNodeDistance(playerNode,destinationNode) <= 1 )
        {
            if(previousClickedNode != null && GameController.Game.levelController.Level.GetNodeDistance(previousClickedNode, n) < 3)
            {
                Debug.Log(Level.GetVectorByDirection(direction));
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Level.GetVectorByDirection(direction));
                playerNode.MoveableObject.MoveableGameObject.transform.rotation = rotation;
=======
=======
>>>>>>> nodeSystem
=======
>>>>>>> nodeSystem
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
                
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> nodeSystem
=======
>>>>>>> nodeSystem
=======
>>>>>>> nodeSystem
                GameController.Game.levelController.Level.MovePlayer(destinationNode);
                previousClickedNode = n;
            }
            else if(previousClickedNode == null)
            {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
                Debug.Log(Level.GetVectorByDirection(direction));
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Level.GetVectorByDirection(direction));
                playerNode.MoveableObject.MoveableGameObject.transform.rotation = rotation;
=======
>>>>>>> nodeSystem
=======
>>>>>>> nodeSystem
=======
>>>>>>> nodeSystem
                GameController.Game.levelController.Level.MovePlayer(destinationNode);
                previousClickedNode = n;
            }
            
        }
        else
        {
            return;
        }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
        previousDirection = direction;
>>>>>>> nodeSystem
=======
        previousDirection = direction;
>>>>>>> nodeSystem
=======
        previousDirection = direction;
>>>>>>> nodeSystem
        //Debug.Log(LevelController.Instance.Level.GetNodeDistance(playerNode, destinationNode));

=======
        Node destinationNode = LevelController.Instance.Level.GetNode(n.X, n.Y + 1, n.Z);
        playerNode = LevelController.Instance.GetPlayerNode();
        LevelController.Instance.MovePlayer(destinationNode);
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System
    }
}
