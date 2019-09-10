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

        currentNode = GetComponent<NodeMemberGraphic>().Node;
        Debug.Log("curre" + currentNode.GetPosition());

       
        if(GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, Dir.GetDirectionByVector(step)) != null) {
            destNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, Dir.GetDirectionByVector(step));
        }
        Debug.Log("dest" + destNode.GetPosition());
    }

    private void Move()
    {
        currentNode = GetComponent<NodeMemberGraphic>().Node;
        Debug.Log("curre" + currentNode.GetPosition());


        if (GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, Dir.GetDirectionByVector(step)) != null) {
            destNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, Dir.GetDirectionByVector(step));
        }
        Debug.Log("dest" + destNode.GetPosition());
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
    }
}
