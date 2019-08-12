using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NodeMember {
    Node previousClickedNode = null;
    Node.Direction previousDirection = Node.Direction.UP;
    public Player(int id) : base(id) {
        Debug.Log("PLAYER CREATED");
    }
    public void Move(Node n, Node.Direction dir) {
        Node playerNode = GameController.Game.CurrentLevel.GetNode(x, y, z);
        Node destinationNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir);

        Vector3 forwardVector = Vector3.zero;
        if (previousClickedNode == null) {
            previousClickedNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(playerNode, Node.Direction.DOWN);
        }
        if (GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1) {
            if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3) {
                playerNode.NodeMember.NodeObjectGraphic.transform.rotation = Quaternion.LookRotation(Dir.GetVectorByDirection(facing), Dir.GetVectorByDirection(upDirection));
                upDirection = dir;
                if (!playerNode.HasSamePosition(destinationNode) && GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1) {
                    if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3) {
                        forwardVector = destinationNode.GetPosition() - playerNode.GetPosition();
                        facing = Dir.GetDirectionByVector(forwardVector);
                        GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
                    }

                }

                else if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) != 0) {
                    facing = previousDirection;
                    GameController.Game.CameraController.UpdateGravity(Dir.GetVectorByDirection(facing), Dir.GetVectorByDirection(dir));
                    NodeObjectMoved.Invoke(playerNode);
                    
                }
                if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) != 0) {
                    TurnEventSystem.currentInstance.NextTurn();
                }
                previousClickedNode = n;
                previousDirection = dir;

            }
            
        }
        else if (GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) == 2 && GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) <= 1) {
            playerNode.NodeMember.NodeObjectGraphic.transform.rotation = Quaternion.LookRotation(Dir.GetVectorByDirection(facing), Dir.GetVectorByDirection(upDirection));
            upDirection = dir;
            facing = Dir.Opposite(previousDirection);
            GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
            GameController.Game.CameraController.UpdateGravity(-Dir.GetVectorByDirection(facing), Dir.GetVectorByDirection(dir));
            previousClickedNode = n;
            previousDirection = dir;
            TurnEventSystem.currentInstance.NextTurn();
        }



    }
}
