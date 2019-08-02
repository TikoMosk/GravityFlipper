using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LevelSerializer : MonoBehaviour
{
    private string path;

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log("error");
            }
            else
            {
                //levelData = JsonUtility.FromJson<LevelData>(webRequest.downloadHandler.text);
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
    public Level LoadLevelLocal(string levelFilename) {

        if (File.Exists(Application.persistentDataPath + "/" + levelFilename)) {
            string s = File.ReadAllText(Application.persistentDataPath + "/" + levelFilename);
            LevelData levelData = JsonUtility.FromJson<LevelData>(s);
            Level level = DeserializeLevel(levelData);
            return level;
        }
        else {
            Debug.LogError("No such file exists to load from");
            return null;
        }
        
    }
    public void SaveLevelLocal(Level level) {
        path = Application.persistentDataPath + "/" + "level1";
        LevelData levelData = SerializeLevel(level);
        string str = JsonUtility.ToJson(levelData);
        System.IO.File.WriteAllText(path, str);

    }
    private LevelData SerializeLevel(Level level) {
        LevelData levelData = new LevelData(level.Width,level.Height,level.Length);
        for (int x = 0; x < level.Width; x++) {
            for (int y = 0; y < level.Height; y++) {
                for (int z = 0; z < level.Length; z++) {
                    if(level.NodeMap[x,y,z].Type != 0) {
                        NodeData nodeData = new NodeData(level.NodeMap[x, y, z]);
                        levelData.nodeDataList.Add(nodeData);
                    }
                    if(level.NodeMap[x,y,z].NodeMember != null) {
                        if (level.NodeMap[x, y, z].NodeMember.Id != 0) {
                            NodeMemberData nodeMemberData = new NodeMemberData(level.NodeMap[x, y, z].NodeMember,x,y,z);
                            levelData.nodeMemberDataList.Add(nodeMemberData);
                        }
                    }
                    

                }
            }
        }
        return levelData;
    }
    private Level DeserializeLevel(LevelData levelData) {
        Level level  = new Level(levelData.width, levelData.height,levelData.length);
        level.InitializeLevel();
        int x, y, z;
        for (int i = 0; i < levelData.nodeDataList.Count; i++) {
            NodeData nodeData = levelData.nodeDataList[i];
            x = nodeData.x;
            y = nodeData.y;
            z = nodeData.z;
            level.SetNode(x, y, z, nodeData.id);
        }
        for (int i = 0; i < levelData.nodeMemberDataList.Count; i++) {
            NodeMemberData nodeMemberData = levelData.nodeMemberDataList[i];
            x = nodeMemberData.x;
            y = nodeMemberData.y;
            z = nodeMemberData.z;
            NodeMemberFactory factory = new NodeMemberFactory();
            level.AddNodeMember(x,y,z,factory.CreateNodeMember(nodeMemberData.id));
        }
        
        return level;
    }

    
}
[System.Serializable]
public class Data {
    
}
[System.Serializable]
public class NodeData{
    public int x;
    public int y;
    public int z;
    public int id;
    public NodeData() {

    }
    public NodeData ( Node n) {
        x = n.X;
        y = n.Y;
        z = n.Z;
        id = n.Type;
        //TODO: DIRECTIONS
        //direction = n.direction;
    }
}
[System.Serializable]
public class NodeMemberData{
    public int x;
    public int y;
    public int z;
    public int id;
    public NodeMemberData() {

    }
    public NodeMemberData(NodeMember nm,int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;
        id = nm.Id;
        //TODO: DIRECTIONS
        //direction = nm.direction;
    }
}
[System.Serializable]
public class LevelData {
    public int width = 0;
    public int height = 0;
    public int length = 0;
    public LevelData() {

    }
    public LevelData(int width, int height, int length) {
        this.width = width;
        this.height = height;
        this.length = length;
        nodeDataList = new List<NodeData>();
        nodeMemberDataList = new List<NodeMemberData>();
    }
    [SerializeField]
    public List<NodeData> nodeDataList;
    [SerializeField]
    public List<NodeMemberData> nodeMemberDataList;
}
