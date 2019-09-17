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
    private Vector3 nextPlatform;
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
        nextPlatform = destination - Vector3.up;

        if (GameController.Game.CurrentLevel.GetNode(destination) == null
            || GameController.Game.CurrentLevel.GetNode(destination).Id != 0
            || GameController.Game.CurrentLevel.GetNode(nextPlatform).Id == 0)
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
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
    }

    private void OnDestroy()
    {
        EventController.currentInstance.Remove(Check);
    }
}
