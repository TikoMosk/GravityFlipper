using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject
{
    private int id;
    public int Id { get => id; set => id = value; }

    private GameObject moveable_go;
    public GameObject Moveable_go { get => moveable_go; set => moveable_go = value; }

    private Action<Node> moveableObjectMoved;
    public Action<Node> objectMoved { get => moveableObjectMoved; set => moveableObjectMoved = value; }

    public MoveableObject(int id)
    {
        this.id = id;
    }
    public void SubscribeToMoveableObjectMoved(Action<Node> objectMoved)
    {
        this.objectMoved += objectMoved;
    }
}
