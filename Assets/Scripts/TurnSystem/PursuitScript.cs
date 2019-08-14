using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitScript : MonoBehaviour
{
    private bool isPursuitOn;
    private Vector3[] directions;
    private Node destNode;
    private Node currentNode;

    private void Start()
    {
        EventController.currentInstance.Register(Check);

        directions = new Vector3[6];
        directions[0] = new Vector3(1, 0, 0);
        directions[1] = new Vector3(0, 1, 0);
        directions[2] = new Vector3(0, 0, 1);
        directions[3] = new Vector3(-1, 0, 0);
        directions[4] = new Vector3(0, -1, 0);
        directions[5] = new Vector3(0, 0, -1);
    }

    public void StartPursuit()
    {
        isPursuitOn = true;
        destNode = GameController.Game.CurrentLevel.Player.NodeObjectGraphic.Node;
        GetComponent<Animator>().SetBool("Chasing", isPursuitOn);
    }

    public void EndPursuit()
    {
        isPursuitOn = false;
        Debug.Log("EndPursuit");
        GetComponent<Animator>().SetBool("Chasing", isPursuitOn);
    }

    private void Check()
    {
        if (!isPursuitOn)
        {
            Vector3 vector = new Vector3();
            foreach (var step in directions)
            {
                vector = transform.position + step;
                if (GameController.Game.CurrentLevel.GetNode((int)vector.x, (int)vector.y, (int)vector.z).NodeMember != null)
                {
                    if (GameController.Game.CurrentLevel.GetNode((int)vector.x, (int)vector.y, (int)vector.z).NodeMember.Id == 1)
                    {
                        StartPursuit();
                    }
                }
            }
        }
        else
        {
            Chase();
        }
    }

    private void Chase()
    {
        currentNode = GameController.Game.CurrentLevel.GetNode((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        Debug.Log("From" + currentNode.Z + "To" + destNode.Z);
        Debug.Log("Pursuit");
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
        destNode = GameController.Game.CurrentLevel.Player.NodeObjectGraphic.Node;
    }
}
