using System;
using System.Collections;
using System.Collections.Generic;
using VolumetricLines;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    private bool isOpen;
    private void Start()
    {
        EventController.currentInstance.Register(Change);
    }

    private void Update()
    {
        if(isOpen)
        {
            GetComponentInChildren<Animator>().SetBool("Enabled", true);

            if (GameController.Game.SmoothGraphics.AnimationCount == 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.up, out hit, 1))
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


    }

    private void Change()
    {
        isOpen = !isOpen;
        GetComponentInChildren<Animator>().SetBool("Enabled", false);
    }
}
