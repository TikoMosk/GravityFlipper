using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public bool pushed;
    public MonoBehaviour friend;
    public GameObject child;

    private void Start()
    {
        EventController.currentInstance.Register(Check);
    }

    private void Check()
    {
        if (IsPlayerNear())
        {
            Debug.Log("PlayerNear pressure pad");
            pushed = true;
            
            if (friend is ILeverFriend)
                ((ILeverFriend)friend).Invoke();
        }
        else
        {
            pushed = false;
        }

        child.GetComponent<Animator>().SetBool("Enabled", pushed);
    }

    private bool IsPlayerNear()
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
