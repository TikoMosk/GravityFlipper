using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    private int levelWidth;
    private int levelHeight;
    private int levelLength;
    public Node[,,] nodeMap;
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
                
>>>>>>> nodeSystem
=======
                
>>>>>>> nodeSystem
=======
                
>>>>>>> nodeSystem
                if(moveableObject.Id == 1)
                {
                    playerNode = nodeMap[x, y, z];
                }
=======
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System
                return true;
            }
        }
        return false;
    }
<<<<<<< HEAD
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            TurnEventSystem.currentInstance.NextTurn();
=======
>>>>>>> nodeSystem
=======
>>>>>>> nodeSystem
=======
>>>>>>> nodeSystem
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
=======
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System
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
