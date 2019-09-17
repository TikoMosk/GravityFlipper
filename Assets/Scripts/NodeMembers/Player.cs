using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const int WIN_BLOCK_ID = 3;
    Node previousClickedNode = null;
    Node.Direction previousDirection = Node.Direction.UP;
    Node.Direction facing;
    Node.Direction upDirection;
    private NodeMemberGraphic graphic;
    private NodeMember playerMember;
    public bool created = false;

    public NodeMemberGraphic Graphic { get => graphic; }
    public Node.Direction Facing { get => facing; }
    public Node.Direction UpDirection { get => upDirection; }

    private void Start() {
        if (GetComponent<NodeMemberGraphic>() != null) {
            graphic = GetComponent<NodeMemberGraphic>();
        }
        else {
            Debug.LogError("Can not find nodeMemberGraphic attached");
        }
        facing = graphic.Node.NodeMember.Facing;
        upDirection = graphic.Node.NodeMember.UpDirection;
        previousClickedNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(graphic.Node, Dir.Opposite(upDirection));
        previousDirection = upDirection;
        playerMember = graphic.Node.NodeMember;
        playerMember.SubscribeToNodeObjectDestroyed(OnKilled);
        created = true;
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
        if (GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1 && GameController.Game.CurrentLevel.CanMoveObject(playerNode, destinationNode)) {
            if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3) {
                playerMember.UpDirection = dir;
                if (!playerNode.HasSamePosition(destinationNode) && GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1) {
                    if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3) {
                        forwardVector = destinationNode.GetPosition() - playerNode.GetPosition();
                        playerMember.Facing = Dir.GetDirectionByVector(forwardVector);
                        GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
                        CheckIfPlayerWon(destinationNode);
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
            Debug.Log("MOVES DOWN");
            playerMember.UpDirection = dir;
            playerMember.Facing = Dir.Opposite(previousDirection);
            GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
            GameController.Game.CameraController.UpdateGravity(-Dir.GetVectorByDirection(playerMember.Facing), Dir.GetVectorByDirection(dir));
            previousClickedNode = n;
            previousDirection = dir;
            CheckIfPlayerWon(destinationNode);
            TurnEventSystem.currentInstance.NextTurn();

        }



    }
    private void CheckIfPlayerWon(Node destNode) {
        if (destNode.Id == WIN_BLOCK_ID) {
            GameController.Game.Win();
        }
    }
    private void OnKilled() {
        PauseMenu.currentInstance.GameOver();
    }

}
