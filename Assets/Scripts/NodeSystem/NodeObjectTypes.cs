using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NodeObjectTypes : MonoBehaviour
{
    private static NodeObjectTypes _instance;
    public static NodeObjectTypes Instance { get { return _instance; } }
    public List<BlockData> blockPrefabs = new List<BlockData>();
    [System.Serializable]
    public struct BlockData
    {
        public string id;
        public GameObject blockPrefab;
    }
    
    //SINGLETON
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //TODO: Get the nodeObjects from a file and return the appropriate nodeObject object and GameObject
    public NodeObject CreateNodeObjectById(string _id)
    {
        NodeObject returnObject = null;
        switch (_id)
        {
            case "": returnObject = null;
                break;
            case "Block": returnObject = new Block() ;
                break;
            case "Player": returnObject = new Player();
                break;
        }
        if(returnObject == null)
        {
            Debug.LogError("The Nodeobject by the given id does not exist");
        }
        returnObject.SetNodeObjectType(_id);
        return returnObject;
    }
    public GameObject GetPrefabById(string _id)
    {
        for (int i = 0; i < blockPrefabs.Count; i++)
        {
            if(blockPrefabs[i].id == _id)
            {
                return blockPrefabs[i].blockPrefab;
            }
        }
        Debug.LogError("No Prefab for given id");
        return null;
    }
}
