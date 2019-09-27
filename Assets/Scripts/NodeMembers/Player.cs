using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject child;
    public bool isDead;

    private const int WIN_BLOCK_ID = 2;
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
    public NodeMember PlayerMember { get => playerMember; set => playerMember = value; }

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
        PlayerMember = graphic.Node.NodeMember;
        PlayerMember.SubscribeToNodeObjectDestroyed(OnKilled);
        created = true;
    }
    public void Move(Node n, Node.Direction dir) {
        Node playerNode = GameController.Game.CurrentLevel.GetNode(transform.position);
        PlayerMember = playerNode.NodeMember;
        if (GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir) == null) {
            return;
        }
        Node destinationNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir);
        facing = PlayerMember.Facing;
        upDirection = PlayerMember.UpDirection;
        if (!destinationNode.Walkable) {
            return;
        }

        Vector3 forwardVector = Vector3.zero;
        if (previousClickedNode == null) {
            previousClickedNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(playerNode, Node.Direction.DOWN);
        }
        if (GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1 && GameController.Game.CurrentLevel.CanMoveObject(playerNode, destinationNode)) {
            if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3) {
                PlayerMember.UpDirection = dir;
                if (!playerNode.HasSamePosition(destinationNode) && GameController.Game.LevelController.Level.GetNodeDistance(playerNode, destinationNode) <= 1) {
                    if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) < 3) {
                        forwardVector = destinationNode.GetPosition() - playerNode.GetPosition();
                        PlayerMember.Facing = Dir.GetDirectionByVector(forwardVector);
                        GameController.Game.CurrentLevel.MoveObject(playerNode, destinationNode);
                        playerMember = destinationNode.NodeMember;
                        CheckIfPlayerWon(destinationNode);
                    }

                }
                else if (GameController.Game.LevelController.Level.GetNodeDistance(previousClickedNode, n) != 0) {
                    PlayerMember.Facing = previousDirection;
                    GameController.Game.CameraController.UpdateGravity(Dir.GetVectorByDirection(PlayerMember.Facing), Dir.GetVectorByDirection(dir));
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
            if (GameController.Game.LevelController.Level.GetNodeInTheDirection(destinationNode, PlayerMember.UpDirection).Id == 0) {
                Node intermediateNode = GameController.Game.LevelController.Level.GetNodeInTheDirection(destinationNode, PlayerMember.UpDirection);

                GameController.Game.CurrentLevel.MoveObject(playerNode, intermediateNode);
                PlayerMember.UpDirection = dir;
                PlayerMember.Facing = Dir.Opposite(previousDirection);
                GameController.Game.CurrentLevel.MoveObject(intermediateNode, destinationNode);
                playerMember = destinationNode.NodeMember;

                GameController.Game.CameraController.UpdateGravity(-Dir.GetVectorByDirection(PlayerMember.Facing), Dir.GetVectorByDirection(dir));

                previousClickedNode = n;
                previousDirection = dir;
                CheckIfPlayerWon(destinationNode);
                TurnEventSystem.currentInstance.NextTurn();
            }


        }
    }
    private void CheckIfPlayerWon(Node destNode) {
        if (destNode.Id == WIN_BLOCK_ID) {
            GameController.Game.Win();
        }
    }
    public void OnKilled() {
        GameController.Game.AudioController.PlayDeathSound();
        child.GetComponent<Animator>().SetBool("isDead", true);
        StartCoroutine(KillAfterAnim());
    }
    IEnumerator KillAfterAnim() {
        GameController.Game.SmoothGraphics.AnimationCount = 1;
        yield return new WaitForSeconds(2f);
        
        PauseMenu.currentInstance.GameOver();
    }

}
