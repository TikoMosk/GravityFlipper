using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSerializer : MonoBehaviour
{
    private int levelWidth = 10;
    private int levelHeight = 10;
    private int levelLength = 10;
    private LevelData levelData;
    private string path;
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
        
    }
    public void SaveLevel(Level level, string filename)
    {
        string contents = "";
        path = Application.persistentDataPath + "/" + filename;
        CreateLevelData(level.LevelWidth, level.LevelHeight, level.LevelLength,level);
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                for (int z = 0; z < levelLength; z++)
                {
                    contents += JsonUtility.ToJson(levelData.nodeDataMap[x,y,z], false);
                }
            }
        }
        System.IO.File.WriteAllText(path, contents);
        Debug.Log("SAVED FILE TO " + path);
    }
    public LevelData LoadLevel(string levelFileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + levelFileName)){
            string content = File.ReadAllText(Application.persistentDataPath + "/" + levelFileName);
            char[] charSeperators = new char[] { '{'};
            string[] contents = content.Split(charSeperators, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = "{" + contents[i];
                
            }
            levelData = new LevelData();
            levelData.nodeDataMap = new NodeData[levelWidth, levelHeight, levelLength];
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    for (int z = 0; z < levelLength; z++)
                    {
                        NodeData n = new NodeData();
                        n = JsonUtility.FromJson<NodeData>(contents[x * (levelWidth * levelLength) + y * levelLength + z]);
                        levelData.nodeDataMap[x, y, z] = n;
                    }
                }
            }
            return levelData;

        }
        Debug.LogError("Error has occured: No file exists to load");
        return null;

    }
}
[System.Serializable]
public class NodeData
{
    public int blockId = 0;
    public int moveableId = 0;

    public NodeData()
    {

    }

}
[System.Serializable]
public class LevelData
{
    public NodeData[,,] nodeDataMap;
}
