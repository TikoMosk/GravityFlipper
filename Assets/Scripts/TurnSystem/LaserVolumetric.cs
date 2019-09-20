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
            dir = Dir.GetVectorByDirection(graphic.Node.NodeMember.Facing);
            if (GameController.Game.SmoothGraphics.AnimationCount == 0)
            {
                RaycastHit hit;
                vl.EndPos = transform.InverseTransformPoint(transform.position + dir) * 50;
                if (Physics.Raycast(transform.position, dir, out hit, 50f))
                {
                    vl.EndPos = transform.InverseTransformPoint(hit.point); ;
                    if (hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>() != null)
                    {
                        if (GameController.Game.LevelController.Factory.isLiving(hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember.Id)) {
                            Destroy(hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().gameObject);
                            if (hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember.Id == 1) {
                                PauseMenu.currentInstance.GameOver();
                            }
                            hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember = null;
                        }
                        
                    }
                }

                Debug.DrawRay(transform.position, dir, Color.blue);
            }
        }
        

    }

}
