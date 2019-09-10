using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Node locationNode;
    Node n;
    int nodeCount;
    bool isGoingToDie = false;
    private void Start() {
        GameController.Game.CameraController.RegisterToGravityChange(Fall);
    }
    private void Fall() {
        locationNode = GetComponent<NodeGraphic>().Node;
        n = FindNodeToFallTo(locationNode, Dir.GetDirectionByVector(-GameController.Game.CameraController.UpVector));
        StartCoroutine(GameController.Game.SmoothGraphics.Fall(transform.GetChild(0).transform, n.GetPosition(),1f/nodeCount));
        StartCoroutine(MoveWhenAnimationEnds());
        
    }
    private void Update() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.GetChild(0).transform.position, 0.45f);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].GetComponent<NodeMemberGraphic>() != null) {
                
                hitColliders[i].GetComponent<NodeMemberGraphic>().Node.NodeMember.Destroy();
            }
        }
    }
    IEnumerator MoveWhenAnimationEnds() {
        while(GameController.Game.SmoothGraphics.AnimationCount > 0) {
            
            yield return null;
        }
        n.SetNodeType(locationNode.Id);
        locationNode.SetNodeType(0);
        GetComponent<NodeGraphic>().Node = n;
        nodeCount = 0;  
    }
    private Node FindNodeToFallTo(Node locationNode, Node.Direction dir) {
        Node n = GameController.Game.CurrentLevel.GetNodeInTheDirection(locationNode, dir);
        
        if(n == null) {
            isGoingToDie = true;
            return locationNode;
        }
        nodeCount++;
        if (n.Id == 0) {
            if(n.NodeMember != null) {
                //n.NodeMember.Destroy();
            }
            return FindNodeToFallTo(n, dir);
        }
        else {
            return locationNode;
        }
    }
    private void OnDestroy() {
        GameController.Game.CameraController.UnRegisterFromGravityChange(Fall);
    }

}
