using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool pushed;
    public SpikeBlock spike;


    public void TurnTheLever()
    {
        if (IsPlayerNear())
        {
            GameObject child = GetComponentInChildren<Transform>().gameObject;
            if (!pushed)
            {
                child.GetComponentsInChildren<Animator>()[0].SetBool("Enabled", true);
                Debug.Log("Hi");
                pushed = true;
                spike.AwakeSpikes();
            }

            else
            {
                child.GetComponentsInChildren<Animator>()[0].SetBool("Enabled", false);
                Debug.Log("Bye");
                pushed = false;
                spike.CloseSpikes();
            }
        }

    }


    public bool IsPlayerNear()
    {
        Transform[] sides = GetComponentsInChildren<Transform>();
        Node nextNode;
        foreach (Transform side in sides)
        {
            if (side.gameObject.tag == "Side")
            {
                nextNode = GameController.Game.CurrentLevel.GetNode(side.position);
                if (nextNode != null && nextNode.NodeMember != null)
                {
                    if (nextNode.NodeMember.Id == 1)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
