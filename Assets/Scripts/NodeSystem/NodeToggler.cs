using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeToggler : MonoBehaviour {
    public GameObject child;
    private Node connectedNode;
    private NodeMember connectedNodeMember;
    private bool pushed;
    private Color32 connectionColor = Color.clear;
    public Color32 ConnectionColor { get => connectionColor; set => connectionColor = value; }
    public void ConnectNode(Node n) {
        connectedNode = null;
        connectedNodeMember = null;
        GameController.Game.CurrentLevel.RemoveNodeConnection(this);
        if (n.NodeGraphic != null) {
            if (n.NodeGraphic.GetComponent<NodeToggleReceiver>() != null) {
                connectedNode = n;
            }
        }
        else if (n.NodeMember != null) {
            if (n.NodeMember.NodeObjectGraphic.GetComponent<NodeToggleReceiver>() != null) {
                connectedNodeMember = n.NodeMember;
            }
        }
        else {
            return;
        }
        GameController.Game.CurrentLevel.AddNodeConnection(this);
    }
    public Vector3 GetConnectNodePosition() {
        if (connectedNode != null) {
            return connectedNode.GetPosition();
        }
        else if (connectedNodeMember != null) {
            return connectedNodeMember.LocationNode.GetPosition();
        }
        else {
            return new Vector3(-1,-1,-1);
        }
    }
    public Vector3 GetPos() {
        return transform.position;
    }
    public void Toggle() {
        if(GameController.Game.SmoothGraphics.AnimationCount == 0) {
            pushed = !pushed;
            child.GetComponent<Animator>().SetBool("Enabled", pushed);
            if (connectedNode != null) {
                connectedNode.NodeGraphic.GetComponent<NodeToggleReceiver>().Trigger();
            }
            else if (connectedNodeMember != null) {
                connectedNodeMember.NodeObjectGraphic.GetComponent<NodeToggleReceiver>().Trigger();
            }
        }
        

    }
    public bool CheckIfSameConnectedNode(Node n) {
        if (connectedNode == n) {
            return false;
        }
        else {
            return true;
        }
    }



}
