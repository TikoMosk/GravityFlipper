using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    public enum Direction
    {
        X,
        Y,
        Z,
    }
    public Direction direction;

    private bool _active;
    private Vector3 step;
    private Vector3 destination;
    private Node nextPlatform;
    private Node currentNode;
    private Node destNode;

    private void Start()
    {
        EventController.currentInstance.Register(Check);

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
    }

    public void Check()
    {
        currentNode = GameController.Game.CurrentLevel.GetNode(transform.position);
        destination = transform.position + step;
        var node = GameController.Game.CurrentLevel.GetNode(destination);
        var a = -GameController.Game.CameraController.UpVector;
        nextPlatform = GameController.Game.CurrentLevel.GetNodeInTheDirection(node, Dir.GetDirectionByVector(a));

        Debug.Log("nextplatform" + node.GetPosition());
        Debug.Log("undernextplatform" + nextPlatform.GetPosition());


        if ((GameController.Game.CurrentLevel.GetNode(destination) == null
            || node.Id != 0
            || nextPlatform.Id == 0))
        {
            step = -step;
            destination = transform.position + step;

        }

        destNode = GameController.Game.CurrentLevel.GetNode(destination);
        _active = !_active;

        Move();
    }

    private void Move()
    {

        if (destNode.NodeMember != null && destNode.NodeMember.Id == 1)
        {
            GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
            StartCoroutine(KillAfterAnim());
            return;
        }

        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);

    }
    IEnumerator KillAfterAnim()
    {
        while (GameController.Game.SmoothGraphics.AnimationCount != 0)
        {
            yield return null;
        }
        PauseMenu.currentInstance.GameOver();

    }

    private void OnDestroy()
    {
        EventController.currentInstance.Remove(Check);
    }
}
