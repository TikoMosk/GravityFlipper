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
    /// <summary>
    /// Moves the player to the given Node
    /// </summary>
    /// <param name="dest"></param>
    public void MovePlayer(Node dest)
    {
        if(playerNode != null)
        {
            playerNode.MoveObjectTo(dest);
            //playerNode.NodeGraphic
            playerNode = dest;
        }
        else
        {
            Debug.LogError("No player found in the level");
        }
    }
    public Node GetPlayerNode()
    {
        return playerNode;
    }
    /// <summary>
    /// Sets the Node Type
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="type"></param>
    public void SetNode(int x, int y, int z, int type)
    {
        if(IsInLevelBounds(x,y,z))
        {
            nodeMap[x, y, z].SetNodeType(type);
        }
        else
        {
            Debug.LogError("Trying to set a Node that is out of level bounds");
        }
    }
    /// <summary>
    /// Gets the Node Type
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Node GetNode(int x, int y, int z)
    {
        if (IsInLevelBounds(x, y, z))
        {
            return nodeMap[x, y, z];
        }
        else
        {
            Debug.Log("Trying to get a Node that is out of level bounds");
            return nodeMap[0,0,0];
        }
    }
    /// <summary>
    /// Gets the distance between 2 Nodes
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int GetNodeDistance(Node a, Node b)
    {
        int distance = Mathf.Abs(b.X - a.X) + Mathf.Abs(b.Y - a.Y) + Mathf.Abs(b.Z - a.Z);
        return distance;
    }
    
    /// <summary>
    /// Checks if the given x, y and z are within the level bounds
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    private bool IsInLevelBounds(int x, int y, int z)
    {
        if(x >= 0 && x < levelWidth && y >= 0 && y < levelHeight && z >= 0 && z < levelLength)
        {
            return true;
        }
        return false;
    }

    public Node GetNodeInTheDirection(Node n, Node.Direction dir)
    {
        if (dir == Node.Direction.RIGHT) return GetNode(n.X + 1, n.Y, n.Z);
        else if (dir == Node.Direction.LEFT) return GetNode(n.X - 1, n.Y, n.Z);
        else if (dir == Node.Direction.UP) return GetNode(n.X, n.Y + 1, n.Z);
        else if (dir == Node.Direction.DOWN) return GetNode(n.X, n.Y - 1, n.Z);
        else if (dir == Node.Direction.FORWARD) return GetNode(n.X, n.Y, n.Z + 1);
        else if (dir == Node.Direction.BACK) return GetNode(n.X, n.Y, n.Z - 1);
        else return n;
    }
    public static Vector3 GetVectorByDirection(Node.Direction dir)
    {
        if (dir == Node.Direction.RIGHT)
        {
            return new Vector3(1, 0, 0);
        }
        else if (dir == Node.Direction.LEFT)
        {
            return new Vector3(-1, 0, 0);
        }
        else if (dir == Node.Direction.UP)
        {
            return new Vector3(0, 1, 0);
        }
        else if (dir == Node.Direction.DOWN)
        {
            return new Vector3(0, -1, 0);
        }
        else if (dir == Node.Direction.FORWARD)
        {
            return new Vector3(0, 0, 1);
        }
        else if (dir == Node.Direction.BACK)
        {
            return new Vector3(0, 0, -1);
        }
        else
        {
            Debug.Log("ERROR");
            return Vector3.zero;
        }
    }


}
