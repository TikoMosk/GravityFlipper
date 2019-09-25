using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelSerializer : MonoBehaviour
{
    private string tempLevel;
    private string tempUsername;
    private int levelsCount = 0;
    private string usr = "";
    private bool ret;

    Level level;

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
                Debug.Log(webRequest.downloadHandler.text.Replace("\\", ""));
                LevelData levelData = JsonUtility.FromJson<LevelData>(webRequest.downloadHandler.text);
                level = DeserializeLevel(levelData);
                //Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }

    public void LoadDevLevel(int levelNumber)
    {
        string serverURL = "localhost:3000/";
        StartCoroutine(GetRequest(serverURL + levelNumber.ToString()));
    }

    public Level LoadLevelLocal(string path)
    {
        
        string result = null;

        string filePath = Path.Combine(Application.streamingAssetsPath, path);

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest reader = new UnityWebRequest(filePath);

            while (!reader.isDone) { }
            result = reader.downloadHandler.text;
        }
        else
        {
            result = File.ReadAllText(filePath);
            if (result != null || result != "")
            {
                
                Debug.Log(result);
            }
        }
        LevelData levelData = JsonUtility.FromJson<LevelData>(result);
        Level level = DeserializeLevel(levelData);
        return level;
    }

    IEnumerator InsertNewUser(string username)
    {
        string deviceInfo = SystemInfo.deviceUniqueIdentifier;
        string url = @"http://localhost:3000/newUser";

        var userData = new Dictionary<string, string>
        {
            { "user", username },
            { "device", deviceInfo }
        };

        UnityWebRequest request = UnityWebRequest.Post(url, userData);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else if (request.isDone)
        {
            Debug.Log(request.url);
            Debug.Log("Done.");
        }
    }
    private void UploadNewUser(string username)
    {
        StartCoroutine(InsertNewUser(username));
    }

    IEnumerator GetLevelData(int levelid)
    {
        string url = @"http://localhost:3000/getData/" + levelid;
        string savePath = @"/Users/hp/gravityflipper/Assets/StreamingAssets/" + "level" + levelid + ".json";

        UnityWebRequest con = UnityWebRequest.Get(url);
        yield return con.SendWebRequest();

        if (con.isNetworkError || con.isHttpError)
        {
            Debug.Log(con.error);
        }

        if (con.isDone)
        {
            tempLevel = con.downloadHandler.text;
            tempLevel = tempLevel.Remove(0, 29);
            tempLevel = tempLevel.Remove(tempLevel.Length - 3, 3);
            File.WriteAllText(savePath, tempLevel);
            tempLevel = null;
            Debug.Log("Download is done.");
        }
    }
    public Level LoadLevelFromServer(int levelid)
    {
        StartCoroutine(GetLevelData(levelid));
        return null;
    }

    IEnumerator UpdateUserLevels(int levelid)
    {
        string deviceInfo = SystemInfo.deviceUniqueIdentifier;
        string url = @"http://localhost:3000/updateUserLevels";

        var userData = new Dictionary<string, string>
        {
            { "levelid", levelid.ToString() },
            { "device", deviceInfo }
        };

        UnityWebRequest request = UnityWebRequest.Post(url, userData);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else if (request.isDone)
        {
            Debug.Log(request.url);
            Debug.Log("Done.");
        }
    }
    public void UnlockLevels(int levelid)
    {
        StartCoroutine(UpdateUserLevels(levelid));
    }

    IEnumerator SaveLevelData(int levelid, string path)
    {
        string url = @"http://localhost:3000/setData";
        string levelJSON = File.ReadAllText(path);

        var levelData = new Dictionary<string, string>
        {
            { "levelid", levelid.ToString() },
            { "leveldata", levelJSON }
        };

        UnityWebRequest con = UnityWebRequest.Post(url, levelData);
        yield return con.SendWebRequest();

        if (con.isNetworkError || con.isHttpError)
        {
            Debug.Log(con.error);
        }
        else if (con.isDone)
        {
            Debug.Log(con.url);
            Debug.Log("Done.");
        }
    }
    public void SaveLevelInDB(int levelid, string path)
    {
        StartCoroutine(SaveLevelData(levelid, path));
    }

    IEnumerator TryAddUsername(string username)
    {
        //todo
        string url = @"http://localhost:3000/checkUsername/" + username;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }

        if (request.isDone)
        {
            tempUsername = request.downloadHandler.text;

            Debug.Log(tempUsername);
            Debug.Log("Download is done.");
        }
    }

    IEnumerator GetUserLevels(string deviceid)
    {
        string url = @"http://localhost:3000/getLevelsCount/" + deviceid;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }

        if (request.isDone)
        {
            string s = request.downloadHandler.text;

            Debug.Log(s);
        }
    }

    IEnumerator GetCount()
    {
        ret = true;
        string url = @"http://localhost:3000/getLevelsCount/";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }

        if (request.isDone)
        {
            int.TryParse(request.downloadHandler.text, out levelsCount);
            ret = false;
            Debug.Log(levelsCount);
        }

    }

    public int GetLevelsCount()
    {
        StartCoroutine(GetCount());
        return levelsCount;
    }

    //private void OnGUI()
    //{
    //    usr = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 20), usr, 12);

    //    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 20), "Username: " + usr))
    //    {
    //        UploadNewUser(usr);
    //    }

    //    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 20), "get"))
    //    {
    //        GetLevelsCount();
    //    }
    //}


    public void SaveLevelLocal(string path, Level level)
    {
        if (level == null)
        {
            return;
        }
        string filePath = Path.Combine(Application.streamingAssetsPath, path);
        LevelData levelData = SerializeLevel(level);
        string str = JsonUtility.ToJson(levelData, true);
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }
            File.WriteAllText(filePath, str);
        }
        else
        {
            File.WriteAllText(filePath, str);

        }

    }

    private LevelData SerializeLevel(Level level)
    {
        LevelData levelData = new LevelData(level.Width, level.Height, level.Length);
        for (int x = 0; x < level.Width; x++)
        {
            for (int y = 0; y < level.Height; y++)
            {
                for (int z = 0; z < level.Length; z++)
                {
                    if (level.NodeMap[x, y, z].Id != 0)
                    {
                        NodeData nodeData = new NodeData(level.NodeMap[x, y, z]);
                        levelData.nodeDataList.Add(nodeData);
                    }
                    if (level.NodeMap[x, y, z].NodeMember != null)
                    {
                        if (level.NodeMap[x, y, z].NodeMember.Id != 0)
                        {
                            NodeMemberData nodeMemberData = new NodeMemberData(level.NodeMap[x, y, z].NodeMember, x, y, z);
                            levelData.nodeMemberDataList.Add(nodeMemberData);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < level.NodeTogglers.Count; i++)
        {
            NodeToggler t = level.NodeTogglers[i];
            Vector3 tPos = t.GetPos();
            Vector3 rPos = t.GetConnectNodePosition();
            NodeConnection connection = new NodeConnection((int)tPos.x, (int)tPos.y, (int)tPos.z, (int)rPos.x, (int)rPos.y, (int)rPos.z);
            levelData.nodeConnections.Add(connection);
        }
        return levelData;
    }

    private Level DeserializeLevel(LevelData levelData)
    {
        Level level = new Level(levelData.width, levelData.height, levelData.length);
        level.InitializeLevel();
        int x, y, z;
        Node.Direction facing = Node.Direction.FORWARD;
        Node.Direction upDirection = Node.Direction.UP;

        for (int i = 0; i < levelData.nodeDataList.Count; i++)
        {
            NodeData nodeData = levelData.nodeDataList[i];
            x = nodeData.x;
            y = nodeData.y;
            z = nodeData.z;
            if (nodeData.facing != "" && nodeData.upDirection != "")
            {
                facing = (Node.Direction)int.Parse(nodeData.facing);
                upDirection = (Node.Direction)int.Parse(nodeData.upDirection);
            }
            level.SetNode(x, y, z, nodeData.id, facing, upDirection);
        }
        for (int i = 0; i < levelData.nodeMemberDataList.Count; i++)
        {
            NodeMemberData nodeMemberData = levelData.nodeMemberDataList[i];
            x = nodeMemberData.x;
            y = nodeMemberData.y;
            z = nodeMemberData.z;
            if (nodeMemberData.facing != "" && nodeMemberData.upDirection != "")
            {
                facing = (Node.Direction)int.Parse(nodeMemberData.facing);
                upDirection = (Node.Direction)int.Parse(nodeMemberData.upDirection);
            }

            level.AddNodeMember(x, y, z, nodeMemberData.id, facing, upDirection);
        }
        for (int i = 0; i < levelData.nodeConnections.Count; i++)
        {
            level.NodeConnections.Add(levelData.nodeConnections[i]);
        }

        return level;
    }


}
[System.Serializable]
public class Data
{

}
[System.Serializable]
public class NodeData
{
    public int x;
    public int y;
    public int z;
    public int id;
    public string facing;
    public string upDirection;
    public NodeData()
    {

    }
    public NodeData(Node n)
    {
        x = n.X;
        y = n.Y;
        z = n.Z;
        id = n.Id;
        //TODO: DIRECTIONS
        //direction = n.direction;
        if (!(n.Facing == Node.Direction.FORWARD && n.UpDirection == Node.Direction.UP))
        {
            facing = ((int)n.Facing).ToString();
            upDirection = ((int)n.UpDirection).ToString();
        }
    }

}
[System.Serializable]
public class NodeMemberData
{
    public int x;
    public int y;
    public int z;
    public int id;
    public string facing;
    public string upDirection;
    public NodeMemberData()
    {

    }
    public NodeMemberData(NodeMember nm, int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        id = nm.Id;
        facing = ((int)nm.Facing).ToString();
        upDirection = ((int)nm.UpDirection).ToString();
    }
}
[System.Serializable]
public class LevelData
{
    public int width = 0;
    public int height = 0;
    public int length = 0;
    public LevelData()
    {

    }
    public LevelData(int width, int height, int length)
    {
        this.width = width;
        this.height = height;
        this.length = length;
        nodeDataList = new List<NodeData>();
        nodeMemberDataList = new List<NodeMemberData>();
        nodeConnections = new List<NodeConnection>();
    }
    [SerializeField]
    public List<NodeData> nodeDataList;
    [SerializeField]
    public List<NodeMemberData> nodeMemberDataList;
    [SerializeField]
    public List<NodeConnection> nodeConnections;
}
[System.Serializable]
public class NodeConnection
{
    [System.Serializable]
    public struct NodeCoordinate
    {
        public int x;
        public int y;
        public int z;
    }
    public NodeCoordinate toggler;
    public NodeCoordinate receiver;
    public NodeConnection(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        toggler.x = x1;
        toggler.y = y1;
        toggler.z = z1;
        receiver.x = x2;
        receiver.y = y2;
        receiver.z = z2;

    }
}
