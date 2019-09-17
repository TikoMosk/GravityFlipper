using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool pushed;
    public MonoBehaviour friend;
    public GameObject child;

    public void TurnTheLever()
    {
        if (IsPlayerNear())
        {
            pushed = !pushed;
            child.GetComponent<Animator>().SetBool("Enabled", pushed);

            if (friend is ILeverFriend)
                ((ILeverFriend)friend).Invoke();
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
    private void ConnectTo(ILeverFriend friend) {
        friend.Invoke();
    }
    private void OnMouseDown()
    {
        this.TurnTheLever();
    }
}
