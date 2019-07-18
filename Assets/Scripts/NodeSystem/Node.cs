using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    #region Instance Variables

    public struct NodePosition{
        public int x;
        public int y;
        public int z;
    }
    private NodePosition nodePos;
    private NodeObject nodeObject;

    #endregion

    public Node(int _x, int _y, int _z)
    {
        nodePos.x = _x;
        nodePos.y = _y;
        nodePos.z = _z;
    }
    public Node(int _x, int _y, int _z, NodeObject _nodeObject)
    {
        nodePos.x = _x;
        nodePos.y = _y;
        nodePos.z = _z;
        nodeObject = _nodeObject;
    }
    
    /// <summary>
    /// Sets the nodeObject for Node
    /// </summary>
    /// <param name="_nodeObject"></param>
    public void SetNodeObject (NodeObject _nodeObject)
    {
        nodeObject = _nodeObject;
    }
    /// <summary>
    /// Creates a NodeObject with given id and assigns it to Node
    /// </summary>
    /// <param name="_type"></param>
    public void AddNodeObject(string _type)
    {
        nodeObject = NodeObjectTypes.Instance.CreateNodeObjectById(_type);
    }
    
    /// <summary>
    /// returns the nodeObject located in Node
    /// </summary>
    /// <returns name="World"></returns>
    public NodeObject GetNodeObject()
    {
        return nodeObject;
    }
    /// <summary>
    /// Returns the Node Position
    /// </summary>
    /// <returns></returns>
    public NodePosition GetNodePosition()
    {
        return nodePos;
    }


}
