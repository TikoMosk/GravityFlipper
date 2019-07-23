using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    private int levelWidth;
    public int LevelWidth { get { return levelWidth; } }
    private int levelHeight;
    public int LevelHeight { get { return levelHeight; } }
    private int levelLength;
    public int LevelLength { get { return levelLength; } }
    public Node[,,] nodeMap;
    private Node playerNode;
    public Level(int levelWidth, int levelHeight, int levelLength, Node[,,] nodeMap)
    {
        this.levelWidth = levelWidth;
        this.levelHeight = levelHeight;
        this.levelLength = levelLength;
        this.nodeMap = nodeMap;
    }
    
    public bool AddMoveableObject(int x, int y, int z, MoveableObject moveableObject)
    {
        if(IsInLevelBounds(x,y,z))
        {
            if(nodeMap[x,y,z].MoveableObject == null)
            {
                nodeMap[x, y, z].MoveableObject = moveableObject;
                if(moveableObject.Id == 1)
                {
                    playerNode = nodeMap[x, y, z];
                }
                return true;
            }
        }
        return false;
    }
    public void MovePlayer(Node dest)
    {
        playerNode.MoveObjectTo(dest);
        playerNode = dest;
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
        if (IsInLevelBounds(x, y, z))
        {
            return nodeMap[x, y, z];
        }
        else
        {
            Debug.LogError("Trying to get a Node that is out of level bounds");
            return null;
        }
    }
    public int GetNodeDistance(Node a, Node b)
    {
        int distance = Mathf.Abs(b.X - a.X) + Mathf.Abs(b.Y - a.Y) + Mathf.Abs(b.Z - a.Z);
        return distance;
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
