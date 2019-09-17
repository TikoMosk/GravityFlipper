using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserVolumetric : MonoBehaviour
{
    public Vector3 endPos;
    private VolumetricLineBehavior vl;

    private void Awake()
    {
        vl = GetComponentInChildren<VolumetricLineBehavior>();
        vl.EndPos = endPos;
    }

    //todo
    private void FixedUpdate()
    {
        if (GameController.Game.SmoothGraphics.AnimationCount == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, vl.EndPos, out hit, vl.EndPos.magnitude))
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

            Debug.DrawRay(transform.position, vl.EndPos, Color.blue);
        }

    }

}
