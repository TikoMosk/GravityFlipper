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

    public void Update()
    {
        if (GameController.Game.SmoothGraphics.AnimationCount == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1) || 
            (Physics.Raycast(transform.position, Vector3.back, out hit, 1)))
            {
                if (hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>() != null)
                {
                    Debug.Log("u dead");
                    //Destroy(hit.collider.transform.parent.gameObject);
                    if (hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember.Id == 1)
                    {

                        PauseMenu.currentInstance.GameOver();
                    }
                    hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember = null;

                }
            }

            Debug.DrawRay(transform.position, Vector3.forward, Color.blue);
            Debug.DrawRay(transform.position, Vector3.back, Color.blue);
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

            Debug.Log(nextPlatform);
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
