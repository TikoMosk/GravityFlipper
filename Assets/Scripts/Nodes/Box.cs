using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Node locationNode;
    Node n;
    private void Start() {
        //GameController.Game.CameraController.RegisterToGravityChange(Fall);
    }
    private void Fall() {
        locationNode = GetComponent<NodeGraphic>().Node;
        n = FindNodeToFallTo(locationNode, Dir.GetDirectionByVector(-GameController.Game.CameraController.UpVector));
        StartCoroutine(GameController.Game.SmoothGraphics.MoveSmoothly(transform.GetChild(0).transform, n.GetPosition(), 0.5f));
        StartCoroutine(MoveWhenAnimationEnds());
    }
    IEnumerator MoveWhenAnimationEnds() {
        while(GameController.Game.SmoothGraphics.AnimationCount > 0) {
            yield return null;
        }
        n.SetNodeType(locationNode.Id);
        locationNode.SetNodeType(0);
        locationNode = n;
    }
    private Node FindNodeToFallTo(Node locationNode, Node.Direction dir) {
        Node n = GameController.Game.CurrentLevel.GetNodeInTheDirection(locationNode, dir);
        if(n == null) {
            return locationNode;
        }
        if (n.Id == 0) {
            if(n.NodeMember != null) {
                //n.DestroyNodeMember();
            }
            else {
                return FindNodeToFallTo(n, dir);
            }
        }
        else {
            return locationNode;
        }
        return null;
    }
    private void OnDestroy() {
        GameController.Game.CameraController.UnRegisterFromGravityChange(Fall);
    }

}
