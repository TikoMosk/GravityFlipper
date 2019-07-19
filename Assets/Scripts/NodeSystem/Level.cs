using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private int levelWidth;
    private int levelHeight;
    private int levelLength;
    private Node[,,] nodeMap;

    public Level(int levelWidth, int levelHeight, int levelLength)
    {
        this.levelWidth = levelWidth;
        this.levelHeight = levelHeight;
        this.levelLength = levelLength;
        CreateNodeMap();
    }
    private void CreateNodeMap()
    {
        nodeMap = new Node[levelWidth, levelHeight, levelLength];
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                for (int z = 0; z < levelLength; z++)
                {
                    nodeMap[x, y, z] = new Node(this, x, y, z);
                }
            }
        }
    }
    public bool AddMoveableObject(int x, int y, int z, MoveableObject moveableObject)
    {
        if(IsInLevelBounds(x,y,z))
        {
            if(nodeMap[x,y,z].MoveableObject == null)
            {
                nodeMap[x, y, z].MoveableObject = moveableObject;
                return true;
            }
        }
        return false;
    }
    public void SetNode(int x, int y, int z, int type)
    {
        if(IsInLevelBounds(x,y,z))
        {
            nodeMap[x, y, z].Type = type;
        }
        else
        {
            Debug.LogError("Trying to set a Node that is out of level bounds");
        }
    }
    public Node GetNode(int x, int y, int z)
    {
        if(IsInLevelBounds(x,y,z))
        {
            return nodeMap[x, y, z];
        }
        else
        {
            Debug.LogError("Trying to get a Node that is out of level bounds");
            return null;
        }
    }
    
    private bool IsInLevelBounds(int x, int y, int z)
    {
        if(x >= 0 && x < levelWidth && y >= 0 && y < levelHeight && z >= 0 && z < levelLength)
        {
            return true;
        }
        return false;
    }
    
   
}
