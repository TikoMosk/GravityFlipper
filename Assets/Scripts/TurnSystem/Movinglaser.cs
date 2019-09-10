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
        EventController.currentInstance.Register(MoveToDestNode);

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
    }

    private void MoveToDestNode()
    {
        Debug.Log("MoveToDestNode");
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
        UpdateState();
    }

    private void UpdateState()
    {
        currentNode = destNode;
        step = -step;
        Debug.Log("step = " + step);
        destNode = GameController.Game.CurrentLevel.GetNode(transform.position + step);
    }
}
