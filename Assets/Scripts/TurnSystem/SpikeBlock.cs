using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour, ILeverFriend {
    public bool isOpen;
    Collider col;
    Node currentNode;

    private void Start() {
        currentNode = GameController.Game.CurrentLevel.GetNode(transform.position);
        //EventController.currentInstance.Register(AwakeSpikes);
        col = GetComponent<Collider>();

        if (isOpen) {
            AwakeSpikes();
        }
        GetComponent<NodeToggleReceiver>().RegisterToToggleReceiver(Invoke);
    }
    void OnDestroy() {
        GetComponent<NodeToggleReceiver>().UnRegisterToToggleReceiver(Invoke);
    }
    /*private void Update()
    {
        if(isOpen)
        {
            GetComponentInChildren<Animator>().SetBool("Enabled", true);

            if (GameController.Game.SmoothGraphics.AnimationCount == 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, end, out hit, 1))
                {
                    if (hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>() != null)
                    {
                        Debug.Log("u dead");
                        Destroy(hit.collider.transform.parent.gameObject);
                        //Destroy(transform.GetComponentInParent<Transform>().gameObject);
                        //Destroy(hit.collider.transform.parent.gameObject);
                        if (hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember.Id == 1)
                        {
                            PauseMenu.currentInstance.GameOver();
                        }
                        hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember = null;
                    }
                }

                Debug.DrawRay(transform.position, Vector3.up, Color.blue);
            }
        }


    } */


    public void AwakeSpikes() {
        GetComponentInChildren<Animator>().SetBool("Enabled", true);
        GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, currentNode.UpDirection).Walkable = false;
        if (IsPlayerNear()) {
            StartCoroutine(DestroyAfterAnimation());
        }
        isOpen = true;
    }

    public void CloseSpikes() {
        GetComponentInChildren<Animator>().SetBool("Enabled", false);
        GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, currentNode.UpDirection).Walkable = true;
        if (IsPlayerNear()) {
            StartCoroutine(DestroyAfterAnimation());
        }
        isOpen = false;
    }

    IEnumerator DestroyAfterAnimation() {
        while (GameController.Game.SmoothGraphics.AnimationCount > 0) {
            yield return null;
        }
        PauseMenu.currentInstance.GameOver();
        GetComponentInParent<NodeMemberGraphic>().Node.NodeMember = null;
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

    public void Invoke() {
        if (isOpen) CloseSpikes();
        else AwakeSpikes();
    }



    /*private void Change()
    {
        isOpen = !isOpen;
        GetComponentInChildren<Animator>().SetBool("Enabled", false);
    }*/
}