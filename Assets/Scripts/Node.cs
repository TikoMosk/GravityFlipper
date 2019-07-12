using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private int pointX, pointY, pointZ;
    public Node(GameObject obj, int x, int y, int z)
	{
        nodeObject = obj;
		this.pointX = x;
		this.pointY = y;
		this.pointZ = z;
	}

	public GameObject nodeObject;
	
    internal Vector3 GetNodePosition()
    {
        return new Vector3(pointX, pointY, pointZ);
    }

    public void InstantiateNode()
    {
        Instantiate(nodeObject, GetNodePosition(), Quaternion.identity);
    }

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
