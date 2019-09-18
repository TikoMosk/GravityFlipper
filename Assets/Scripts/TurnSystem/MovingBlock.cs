using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour, ILeverFriend
{
    private Vector3 step;
    private Node currentNode;
    private Node destNode;

    public enum Direction
    {
        X,
        Y,
        Z,
    }
    public Direction direction;
    public bool negative;

    private void Start()
    {
        switch (direction)
        {
            case Direction.X:
                step = Vector3.right;
                break;
            case Direction.Y:
                step = Vector3.up;
                break;
            case Direction.Z:
                step = Vector3.forward;
                break;
        }

        if (negative)
        {
            step = -step;
        }

        currentNode = GameController.Game.CurrentLevel.GetNode(transform.position);
        destNode = GameController.Game.CurrentLevel.GetNode(transform.position + step);
        GetComponent<NodeToggleReceiver>().RegisterToToggleReceiver(Invoke);
    }
    private void OnDestroy() {
        GetComponent<NodeToggleReceiver>().UnRegisterToToggleReceiver(Invoke);
    }

    private void MoveToDestNode()
    {
        Node.Direction upDir = Dir.GetDirectionByVector(GameController.Game.CameraController.UpVector);
        if (GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, upDir).NodeMember != null) {
            GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, upDir).NodeMember.NodeObjectGraphic.transform.parent = this.transform;
            GameController.Game.CurrentLevel.MoveMemberLogically(GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, upDir), GameController.Game.CurrentLevel.GetNodeInTheDirection(destNode, upDir));
            
            StartCoroutine(ReturnToLevelParent(GameController.Game.CurrentLevel.GetNodeInTheDirection(destNode, upDir).NodeMember.NodeObjectGraphic.transform));
        }
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
        UpdateState();
    }

    private IEnumerator ReturnToLevelParent(Transform obj) {
        while(GameController.Game.SmoothGraphics.AnimationCount != 0) {
            yield return null;
        }
        obj.transform.parent = this.transform.parent;
    }

    private void UpdateState()
    {
        Node temp = currentNode;
        currentNode = destNode;
        destNode = temp;
    }

    public void Invoke()
    {
        MoveToDestNode();
    }
}
