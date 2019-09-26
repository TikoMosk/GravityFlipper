using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakBlock : MonoBehaviour
{
    private bool destroy;

    void Awake()
    {
        EventController.currentInstance.Register(Check);
    }

    private void Check()
    {
        if (destroy)
        {
            EventController.currentInstance.Remove(Check);
            GetComponent<NodeGraphic>().Node.SetNodeType(0);
            //Destroy(gameObject);

        }

        if (IsPlayerNear())
        {
            destroy = true;
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
