using System;
using System.Collections;
using System.Collections.Generic;
using VolumetricLines;
using UnityEngine;

public class LaserRay : MonoBehaviour
{
    public float speed;
    public Vector3 endPoint_1;
    public Vector3 endPoint_2;
    public Vector3 currentPos;

    private VolumetricLineBehavior vl;
    public GameObject endObject;

    private void Start()
    {
        EventController.currentInstance.Register(Check);

        vl = GetComponentInChildren<VolumetricLineBehavior>();
        endObject.transform.position = endPoint_1;
        currentPos = endPoint_1;
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
            }
            else
            {
                ChangeDirection(currentPos);
            }
        }

        if (isMoving && endObject.transform.position == Vector3.zero)
        {
            isMoving = false;
            _in = false;
            ChangePos();
            Debug.Log(currentPos);
        }
        else if (isMoving && endObject.transform.position == endPoint_1
            || isMoving && endObject.transform.position == endPoint_2)
        {
            isMoving = false;
            _in = true;
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
