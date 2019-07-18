using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    #region Variables

    private readonly int nodeID;
    private readonly int pointX, pointY, pointZ;
    internal INodeObject nodeObject;

    #endregion

    #region Constructors

    public Node(int x, int y, int z)
    {
        this.nodeID = this.GetHashCode();
        this.nodeObject = null;
        this.pointX = x;
        this.pointY = y;
        this.pointZ = z;
    }

    public Node(INodeObject obj, int x, int y, int z)
    {
        this.nodeID = this.GetHashCode();
        this.nodeObject = obj;
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
        //ADD A TEMPORARY OBJECT
        this.nodeObject = node.nodeObject;
        node.nodeObject = null;
    }

    public void ChangeObject(INodeObject obj)
    {
        this.nodeObject = obj;
    }


    public void AddObject(INodeObject obj)
    {
        if (nodeObject == null)
        {
            this.nodeObject = obj;
        }
        else
        {
            //todo
        }
    }

    #endregion
}
