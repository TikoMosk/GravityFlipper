using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject
{
    private int id;
    public int Id { get => id; set => id = value; }

    private Action<Node> nodeObjectMoved;
    public NodeObjectGraphic NodeObjectGraphic { get => nodeObjectGraphic; set => nodeObjectGraphic = value; }
    public Action<Node> NodeObjectMoved { get => nodeObjectMoved; set => nodeObjectMoved = value; }

    private NodeObjectGraphic nodeObjectGraphic;

    public NodeObject(int id)
    {
        this.id = id;
    }
    public void SubscribeToMoveableObjectMoved(Action<Node> objectMoved)
    {
        NodeObjectMoved += objectMoved;
    }

    public NodeObjectGraphic CreateMoveableObjectGraphic(GameObject nodeObject_GameObject)
    {
        nodeObjectGraphic = nodeObject_GameObject.AddComponent<NodeObjectGraphic>();
        return nodeObjectGraphic;
    }
}
