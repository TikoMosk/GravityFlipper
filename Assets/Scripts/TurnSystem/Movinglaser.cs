using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movinglaser : MonoBehaviour
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

    void Start()
    {
        EventController.currentInstance.Register(Move);

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

        currentNode = GameController.Game.CurrentLevel.GetNode(gameObject.transform.position);
        Debug.Log("curre" + currentNode.GetPosition());

        destNode = GameController.Game.CurrentLevel.GetNode(gameObject.transform.position + step);
        Debug.Log("dest" + destNode.GetPosition());
    }

    private void Move()
    {
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
    }
}
