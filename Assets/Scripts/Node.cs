using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public readonly int nodeID;
    private int pointX, pointY, pointZ;
    public Node(int x, int y, int z)
    {
        nodeID = this.GetHashCode();
        this.pointX = x;
        this.pointY = y;
        this.pointZ = z;
    }
    public Node(GameObject obj, int x, int y, int z)
	{
        nodeID = this.GetHashCode();
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

    #region Transform Methods

    internal void ChangeX(int x)
    {
        this.pointX = x;
    }
    internal void ChangeY(int y)
    {
        this.pointY = y;
    }
    internal void ChangeZ(int z)
    {
        this.pointZ = z;
    }

    internal void ChangePosition(int x, int y, int z)
    {
        this.pointX = x;
        this.pointY = y;
        this.pointZ = z;
    }

    #endregion

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
