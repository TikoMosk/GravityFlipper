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
    private Node currentNode;
    private Node destNode;

    private void Start()
    {
        EventController.currentInstance.Register(Check);

        switch (direction)
        {
            case Direction.X:
                step = new Vector3(1, 0, 0);
                break;
            case Direction.Y:
                step = new Vector3(0, 1, 0);
                break;
            case Direction.Z:
                step = new Vector3(0, 0, 1);
                break;
        }
    }

    public void Check()
    {
        currentNode = GameController.Game.CurrentLevel.GetNode((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);

        destination = transform.position + step;

        if (GameController.Game.CurrentLevel.GetNode((int)destination.x, (int)destination.y, (int)destination.z).Id != 0)
        {
            step = -step;
            destination = transform.position + step;
        }
        destNode = GameController.Game.CurrentLevel.GetNode((int)destination.x, (int)destination.y, (int)destination.z);
        _active = !_active;

        Move();
    }

    private void Move()
    {
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
    }
}
