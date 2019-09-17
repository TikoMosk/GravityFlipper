using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserVolumetric : MonoBehaviour
{
    public Vector3 endPos;
    private Vector3 dir;
    private VolumetricLineBehavior vl;
    NodeMemberGraphic graphic;

    private void Awake()
    {
        vl = GetComponentInChildren<VolumetricLineBehavior>();
        
    }

    //todo
    private void FixedUpdate()
    {
        if(GetComponent<NodeMemberGraphic>() != null)
        {
            graphic = GetComponent<NodeMemberGraphic>();
            dir = Dir.GetVectorByDirection(graphic.Node.Facing);
            if (GameController.Game.SmoothGraphics.AnimationCount == 0)
            {
                RaycastHit hit;
                vl.EndPos = dir * 100f;
                if (Physics.Raycast(transform.position, dir, out hit, 50f))
                {
                    vl.EndPos = hit.point;
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

                Debug.DrawRay(transform.position, dir, Color.blue);
            }
        }
        

    }

}
