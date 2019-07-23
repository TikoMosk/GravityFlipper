using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSerializer : MonoBehaviour
{
    private int levelWidth;
    private int levelHeight;
    private int levelLength;
    private LevelData levelData;
    private string filename = "level.json";
    private string path;
    private string contents = "";
    private void Start()
    {
        path = Application.persistentDataPath + "/" + filename;
    }
    public void CreateLevelData(int levelWidth, int levelHeight, int levelLength, Level level)
    {
        this.levelWidth = levelWidth;
        this.levelHeight = levelHeight;
        this.levelLength = levelLength;
        Node[,,] nodeMap = level.nodeMap;
        levelData = new LevelData();
        levelData.nodeDataMap = new NodeData[levelWidth, levelHeight, levelLength];
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                for (int z = 0; z < levelLength; z++)
                {
                    levelData.nodeDataMap[x, y, z] = new NodeData();
                    levelData.nodeDataMap[x, y, z].blockId = nodeMap[x, y, z].Type;
                    if (nodeMap[x, y, z].MoveableObject != null)
                    {
                        levelData.nodeDataMap[x, y, z].moveableId = nodeMap[x, y, z].MoveableObject.Id;
                    }
                    
                }
            }
        }
        SaveLevel();
    }
    private void SaveLevel()
    {
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                for (int z = 0; z < levelLength; z++)
                {
                    contents += JsonUtility.ToJson(levelData.nodeDataMap[x,y,z], true);
                }
            }
        }
        System.IO.File.WriteAllText(path, contents);
        Debug.Log("SAVED FILE TO " + path);
        Debug.Log("FILE CONTAINTS " + contents);
    }
}
[System.Serializable]
public class NodeData
{
    public int blockId;
    public int moveableId;

    public NodeData()
    {

    }

}
[System.Serializable]
public class LevelData
{
    public NodeData[,,] nodeDataMap;
}
