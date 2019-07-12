using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    #region Variables

    private readonly int nodeID;
    private readonly int pointX, pointY, pointZ;
    internal GameObject nodeObject;

    #endregion

    #region Constructors

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

    #endregion

    #region Standard Methods

    public int GetNodeID()
    {
        return nodeID;
    }

    internal Vector3 GetNodePosition()
    {
        return new Vector3(pointX, pointY, pointZ);
    }

    #endregion

    #region Transform Methods

    public void ChangeObjectByNode(Node node)
    {
        this.nodeObject = node.nodeObject;
        node.nodeObject = null;
    }

    public void ChangeObject(GameObject obj)
    {
        this.nodeObject = obj;
    }

    public void InstantiateNode()
    {
        if (nodeObject != null)
        {
            Instantiate(nodeObject, GetNodePosition(), Quaternion.identity);
        }

        //ToDo
        //Debug.Log("nodeObject is null");
    }

    public void AddObject(GameObject obj)
    {
        if (nodeObject == null)
        {
            this.nodeObject = obj;
        }
    }

    #endregion
}
