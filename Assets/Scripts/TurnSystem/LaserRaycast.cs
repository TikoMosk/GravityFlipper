using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRaycast : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;

    void Start()
    {
        startPos = transform.position;
    }


    void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(startPos, endPos, out hit))
        {
            if(hit.collider.gameObject.tag == "LaserTrigger")
            {
                Debug.Log("u dead");
            }
        }

        Debug.DrawRay(startPos, endPos, Color.blue);
        UpdateTarget();

    }

    private void UpdateTarget()
    {
        endPos = GetComponent<LaserRay>().currentPos;
    }
}
