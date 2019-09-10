using System;
using System.Collections;
using System.Collections.Generic;
using VolumetricLines;
using UnityEngine;

public class LaserRay : MonoBehaviour
{

    public bool isStatic;
    public float speed;
    public Vector3 endPoint_1;
    public Vector3 endPoint_2;
    internal Vector3 currentPos;
    public GameObject endObject;

    private VolumetricLineBehavior vl;
    private bool flag;
    private Vector3 rayendPos;

    private void Start()
    {
        //if (!isStatic)
            EventController.currentInstance.Register(Check);

        vl = GetComponentInChildren<VolumetricLineBehavior>();
        endObject.transform.position = endPoint_1;
        currentPos = endPoint_1;
        rayendPos = endPoint_1;
        UpdateLaser();
    }

    private bool isMoving;
    private bool _in = true;

    private void Check()
    {
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (_in)
            {
                ChangeDirection(Vector3.zero);
                rayendPos = Vector3.zero;
            }
            else
            {
                ChangeDirection(currentPos);
                rayendPos = currentPos;
            }
        }

        if (isMoving && endObject.transform.position == Vector3.zero)
        {
            isMoving = false;
            _in = false;
            ChangePos();
        }
        else if (isMoving && endObject.transform.position == endPoint_1
            || isMoving && endObject.transform.position == endPoint_2)
        {
            isMoving = false;
            _in = true;
        }
    }

    private void FixedUpdate()
    {
        if (GameController.Game.SmoothGraphics.AnimationCount == 0)
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

    private void ChangeDirection(Vector3 endpoint)
    {
        endObject.transform.position = Vector3.MoveTowards(endObject.transform.position, endpoint, speed / 10);
        UpdateLaser();
    }

    private void ChangePos()
    {
        if (currentPos == endPoint_1)
        {
            currentPos = endPoint_2;
        }
        else
        {
            currentPos = endPoint_1;
        }
    }

    private void UpdateLaser()
    {
        vl.EndPos = endObject.transform.position;
        
    }
}
