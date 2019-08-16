using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Node previousClickedNode = null;
    Node.Direction previousDirection = Node.Direction.UP;
    Node.Direction facing;
    Node.Direction upDirection;
    private NodeMemberGraphic graphic;
    private NodeMember playerMember;

    public NodeMemberGraphic Graphic { get => graphic; }

    private void Start() {
        if(GetComponent<NodeMemberGraphic>() != null) {
            graphic = GetComponent<NodeMemberGraphic>();
        }
        else {
            Debug.LogError("Can not find nodeMemberGraphic attached");
        }
        
    }
    public void Move(Node n, Node.Direction dir) {
        Node playerNode = GameController.Game.CurrentLevel.GetNode(transform.position);
        playerMember = playerNode.NodeMember;
        if (GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir) == null) {
            return;
        }
        Node destinationNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir);
        facing = playerMember.Facing;
        upDirection = playerMember.UpDirection;
        

        Vector3 forwardVector = Vector3.zero;
        if (previousClickedNode == null) {
            previousClickedNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(playerNode, Node.Direction.DOWN);
        }
        if (GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1) {
            if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3) {
                playerMember.UpDirection = dir;
                if (!playerNode.HasSamePosition(destinationNode) && GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1) {
                    if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3) {
                        forwardVector = destinationNode.GetPosition() - playerNode.GetPosition();
                        playerMember.Facing = Dir.GetDirectionByVector(forwardVector);
                        GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
                    }

                }

                else if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) != 0) {
                    playerMember.Facing = previousDirection;
                    GameController.Game.CameraController.UpdateGravity(Dir.GetVectorByDirection(playerMember.Facing), Dir.GetVectorByDirection(dir));
                    playerNode.NodeMember.NodeObjectMoved.Invoke(playerNode);
                    
                }
                if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) != 0) {
                    TurnEventSystem.currentInstance.NextTurn();
                }
                previousClickedNode = n;
                previousDirection = dir;

            }
            
        }
        else if (GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) == 2 && GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) <= 1) {
            playerMember.UpDirection = dir;
            playerMember.Facing = Dir.Opposite(previousDirection);
            GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
            GameController.Game.CameraController.UpdateGravity(-Dir.GetVectorByDirection(playerMember.Facing), Dir.GetVectorByDirection(dir));
            previousClickedNode = n;
            previousDirection = dir;
            TurnEventSystem.currentInstance.NextTurn();
        }



    }
    
}
