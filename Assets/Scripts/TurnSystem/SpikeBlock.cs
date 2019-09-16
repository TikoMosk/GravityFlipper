using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour, ILeverFriend
{
    public bool isOpen;
    Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
        //EventController.currentInstance.Register(AwakeSpikes);
        if (isOpen == true)
        {
            AwakeSpikes();
        }

    }

    public void AwakeSpikes()
    {
        collider.enabled = false;
        GetComponentInChildren<Animator>().SetBool("Enabled", true);
        if(IsPlayerNear())
        {
            GetComponentInParent<NodeMemberGraphic>().Node.NodeMember = null;
            //PauseMenu.currentInstance.GameOver();
        }
        isOpen = true;
    }

    public void CloseSpikes()
    {
        collider.enabled = true;
        GetComponentInChildren<Animator>().SetBool("Enabled", false);

        isOpen = false;
    }


    public void Check()
    {
            GetComponentInParent<NodeMemberGraphic>().Node.NodeMember = null;
        PauseMenu.currentInstance.GameOver();
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
                    if (nextNode.NodeMember.Id == 1
                        || nextNode.NodeMember.Id == 2
                        || nextNode.NodeMember.Id == 3)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void Invoke()
    {
        if (isOpen) CloseSpikes();
        else        AwakeSpikes();
    }




}