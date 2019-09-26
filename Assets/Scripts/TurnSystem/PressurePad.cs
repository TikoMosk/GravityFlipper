using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private bool pushed;
    private bool previousPushed;
    Node currentNode;

    private void Start()
    {
        EventController.currentInstance.Register(Check);
        currentNode = GameController.Game.CurrentLevel.GetNode(transform.position);
    }

    private void OnDestroy()
    {
        //EventController.currentInstance.Remove(Check);
    }

    private void Check()
    {
        Debug.Log("check");
        if (IsSmthOn())
        {
            pushed = true;
        }
        else
        {
            pushed = false;
        }
        if (pushed != previousPushed)
        {
            InvokeFriend();
        }
        previousPushed = pushed;
    }

    private void InvokeFriend()
    {
        GetComponent<NodeToggler>().Toggle();
    }

    private bool IsSmthOn()
    {
        if (GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, currentNode.UpDirection).NodeMember != null)
        {
            Debug.Log(GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, currentNode.UpDirection).GetPosition());
            return true;
        }

        return false;
    }
}
