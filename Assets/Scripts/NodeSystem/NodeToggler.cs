using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeToggler : MonoBehaviour
{
    public GameObject child;
    private Node connectedNode;
    private bool pushed;
    public void ConnectNode(Node n) {
        connectedNode = n;
    }
    private void Toggle() {
        if(GameController.Game.SmoothGraphics.AnimationCount == 0) {
            pushed = !pushed;
            child.GetComponent<Animator>().SetBool("Enabled", pushed);
            Debug.Log(connectedNode.NodeGraphic.name);
            if (connectedNode != null && connectedNode.NodeGraphic.GetComponent<NodeToggleReceiver>() != null) {

                connectedNode.NodeGraphic.GetComponent<NodeToggleReceiver>().Trigger();
            }
        }
        
    }
    private void OnMouseDown() {
        if (IsPlayerNear()) {
            Toggle();
        }
    }

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
}
