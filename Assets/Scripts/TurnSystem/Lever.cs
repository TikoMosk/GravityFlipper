using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool IsPlayerNear() {
        Transform[] sides = GetComponentsInChildren<Transform>();
        Node nextNode;
        foreach (Transform side in sides) {
            if (side.gameObject.tag == "Side") {
                nextNode = GameController.Game.CurrentLevel.GetNode(side.position);
                if (nextNode != null && nextNode.NodeMember != null) {
                    if (nextNode.NodeMember.Id == 1) {
                        return true;
                    }
                }
            }
        }

        return false;
    }
    private void OnMouseDown() {
        if (IsPlayerNear()) {
            GetComponent<NodeToggler>().Toggle();
        }
    }
}
