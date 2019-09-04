using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserStattic : MonoBehaviour
{
    private Vector3 rayendPos;
    void Start()
    {
        rayendPos = GetComponentInChildren<VolumetricLineBehavior>().EndPos;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayendPos, out hit, rayendPos.magnitude))
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

        Debug.DrawRay(transform.position, rayendPos, Color.blue);
    }
}
