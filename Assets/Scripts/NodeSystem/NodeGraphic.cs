using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeGraphic : MonoBehaviour
{
<<<<<<< HEAD
    public event Action<Node.Direction> onClick;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

    public void GetClicked(Node.Direction dir)
    {
        onClick?.Invoke(dir);
    }
    public void RotateGraphicToDirection(Node.Direction dir)
    {
=======
    private Node node;

    public Node Node { get => node; set => node = value; }
=======
    public event Action onClick;
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System

    private void OnMouseDown()
    {
<<<<<<< HEAD
        onClick?.Invoke(dir);
    }
    public void RotateGraphicToDirection(Node.Direction dir)
    {
>>>>>>> nodeSystem
=======
    private Node node;

    public Node Node { get => node; set => node = value; }

    public void GetClicked(Node.Direction dir)
    {
        onClick?.Invoke(dir);
    }
    public void RotateGraphicToDirection(Node.Direction dir)
    {
>>>>>>> nodeSystem
=======
    private Node node;

    public Node Node { get => node; set => node = value; }

    public void GetClicked(Node.Direction dir)
    {
        onClick?.Invoke(dir);
    }
    public void RotateGraphicToDirection(Node.Direction dir)
    {
>>>>>>> nodeSystem

=======
        if (onClick != null)
            onClick();
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System
    }
}
