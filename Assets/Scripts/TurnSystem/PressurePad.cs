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
        if (IsSmthOn())
        {
            if (!pushed)
            {
                pushed = true;

                InvokeFriend();
            }
        }
        else
        {
            if (pushed)
                InvokeFriend();

            pushed = false;
        }

        child.GetComponent<Animator>().SetBool("Enabled", pushed);
    }

    private void InvokeFriend()
    {
        if (friend is ILeverFriend)
            ((ILeverFriend)friend).Invoke();
    }

    private bool IsSmthOn()
    {
        Node nextNode;
        Transform side = GetComponentsInChildren<Transform>()[3];

        if (side.gameObject.tag == "Side")
        {
            nextNode = GameController.Game.CurrentLevel.GetNode(side.position);
            if (nextNode != null && nextNode.NodeMember != null)
            {
                if (nextNode.NodeMember.Id != 0)
                {
                    return true;
                }
            }
        }


        return false;
    }
}
