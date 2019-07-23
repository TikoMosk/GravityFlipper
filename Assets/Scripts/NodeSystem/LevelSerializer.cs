using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSerializer : MonoBehaviour
{
    private LevelData levelData;
    private string path;

    //Turns a level into leveldata
    public void CreateLevelData(Level level)
    {
        
        Node[,,] nodeMap = level.nodeMap;
        levelData = new LevelData();
        levelData.levelWidth = level.LevelWidth;
        levelData.levelHeight = level.LevelHeight;
        levelData.levelLength = level.LevelLength;
        levelData.nodeDataMap = new NodeData[levelData.levelWidth, levelData.levelHeight, levelData.levelLength];
        for (int x = 0; x < levelData.levelWidth; x++)
        {
            for (int y = 0; y < levelData.levelHeight; y++)
            {
                for (int z = 0; z < levelData.levelLength; z++)
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
    /// <summary>
    /// Saves the level to the given file
    /// </summary>
    /// <param name="level"></param>
    /// <param name="filename"></param>
    public void SaveLevel(Level level, string filename)
    {
        string contents = "";
        path = Application.persistentDataPath + "/" + filename;
        CreateLevelData(level);
        contents += JsonUtility.ToJson(levelData, false);
        for (int x = 0; x < levelData.levelWidth; x++)
        {
            for (int y = 0; y < levelData.levelHeight; y++)
            {
                for (int z = 0; z < levelData.levelLength; z++)
                {
                    contents += JsonUtility.ToJson(levelData.nodeDataMap[x,y,z], false);
                }
            }
        }
        System.IO.File.WriteAllText(path, contents);
        Debug.Log("SAVED FILE TO " + path);
    }
    /// <summary>
    /// Loads the level from the fileName
    /// </summary>
    /// <param name="levelFileName"></param>
    /// <returns></returns>
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
            levelData = JsonUtility.FromJson<LevelData>(contents[0]);
            Queue<string> contentQueue = new Queue<string>(contents);
            contentQueue.Dequeue();
            contents = contentQueue.ToArray();
            levelData.nodeDataMap = new NodeData[levelData.levelLength, levelData.levelHeight, levelData.levelLength];
            for (int x = 0; x < levelData.levelLength; x++)
            {
                for (int y = 0; y < levelData.levelHeight; y++)
                {
                    for (int z = 0; z < levelData.levelLength; z++)
                    {
                        NodeData n = new NodeData();
                        n = JsonUtility.FromJson<NodeData>(contents[x * (levelData.levelWidth * levelData.levelLength) + y * levelData.levelLength + z]);
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
    public int levelWidth = 0;
    public int levelHeight = 0;
    public int levelLength = 0;
    public NodeData[,,] nodeDataMap;
    public LevelData()
    {

    }
}
