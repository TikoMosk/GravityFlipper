using System;
using System.Collections;
using System.Collections.Generic;
using VolumetricLines;
using UnityEngine;

public class LaserRay : MonoBehaviour
{
    public Action a;
    public Vector3 endPoint_1;
    public Vector3 endPoint_2;
    
    private VolumetricLineBehavior vl;
    private GameObject currentDirObject;
    public GameObject endObject_1;
    public GameObject endObject_2;
    private Node startNode;

    private void Start()
    {
        EventController.currentInstance.Register(Check);

        startNode = GameController.Game.CurrentLevel.GetNode(transform.position);
        vl = GetComponentInChildren<VolumetricLineBehavior>();
        endObject_1.transform.position = endPoint_1;
        endObject_2.transform.position = endPoint_2;

        currentDirObject = endObject_1;
        UpdateLaser();
    }

    private void Check()
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        if (currentDirObject == endObject_1)
            currentDirObject = endObject_2;
        else
            currentDirObject = endObject_1;

        UpdateLaser();
    }

    private void UpdateLaser()
    {
        vl.EndPos = currentDirObject.transform.position;
    }
}
