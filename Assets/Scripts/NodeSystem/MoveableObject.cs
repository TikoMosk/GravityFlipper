﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject
{
    private int id;
    public int Id { get => id; set => id = value; }

    private GameObject moveableGameObject;
    public GameObject MoveableGameObject { get => moveableGameObject; set => moveableGameObject = value; }

    private Action<Node> moveableObjectMoved;
    public Action<Node> objectMoved { get => moveableObjectMoved; set => moveableObjectMoved = value; }
    public MoveableObjectGraphic MoveableObjectGraphic { get => moveableObjectGraphic; set => moveableObjectGraphic = value; }

    private MoveableObjectGraphic moveableObjectGraphic;

    public MoveableObject(int id)
    {
        this.id = id;
    }
    public void SubscribeToMoveableObjectMoved(Action<Node> objectMoved)
    {
        this.objectMoved += objectMoved;
    }
    public void CreateMoveableObjectGraphic(GameObject moveable_go)
    {
        moveableObjectGraphic = moveable_go.AddComponent<MoveableObjectGraphic>();
    }
}
