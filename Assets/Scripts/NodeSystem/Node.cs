using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Level level;
    private int x;
    private int y;
    private int z;
    public int X { get => x;  }
    public int Y { get => y; }
    public int Z { get => z; }
    private int type;
    public int Type { get { return type; }
        set
        {
            type = value;
            if(nodeTypeChanged != null)
            {
                nodeTypeChanged(this);
            }
        }
    }

    private MoveableObject moveableObject;
    public MoveableObject MoveableObject { get { return moveableObject; } set { moveableObject = value; } }
    Action<Node> nodeTypeChanged;

    public Node(Level level, int x, int y, int z)
    {
        this.level = level;
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = 0;
        this.moveableObject = null;
    }
    public Node(Level level, int x, int y, int z, int type, MoveableObject moveAbleObject)
    {
        this.level = level;
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = type;
        this.moveableObject = moveAbleObject;
    }
    public void SubscribeToNodeTypeChanged(Action<Node> nodeTypeChanged)
    {
        this.nodeTypeChanged += nodeTypeChanged;
    }

}
