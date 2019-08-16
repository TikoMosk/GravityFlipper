using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFactory : MonoBehaviour
{

    List<NodeDetails> nodeDetailsList;
    List<NodeDetails> nodeMemberDetailsList;


    private static NodeFactory factory;

    public static NodeFactory Factory { get => factory; set => factory = value; }
    [System.Serializable]
    public enum Category { DecorativeBlock, Living, FunctioningBlock, Logic, Miscellaneous };
    private void Awake()
    {
        if (factory != null && factory != this)
        {
            Destroy(this);
        }
        else
        {
            factory = this;
        }
        nodeDetailsList = new List<NodeDetails>();
        nodeMemberDetailsList = new List<NodeDetails>();
        CreateNodeDetails();
    }
    private void CreateNodeDetails()
    {

        //NODES
        nodeDetailsList.Add(new NodeDetails(
            "Air",
            "",
            "",
            Category.Living
            ));
        nodeDetailsList.Add(new NodeDetails(
            "Block",
            "Block",
            "Block",
            Category.DecorativeBlock
            ));


        //NODEMEMBERS
        nodeMemberDetailsList.Add(new NodeDetails(
            "Air",
            "",
            "",
            Category.Living
            ));
        nodeMemberDetailsList.Add(new NodeDetails(
            "Player",
            "Player",
            "Player",
            Category.Living
            ));
        nodeMemberDetailsList.Add(new NodeDetails(
            "Enemy",
            "EnemySpider",
            "Player",
            Category.Living
            ));
        nodeMemberDetailsList.Add(new NodeDetails(
            "Laser",
            "LaserCube",
            "Player",
            Category.Living
            ));
    }

    public GameObject GetNodePrefabById(int id)
    {
        if (id >= 0 && id < nodeDetailsList.Count)
        {
            GameObject prefab = (GameObject)Resources.Load(nodeDetailsList[id].prefab);
            return prefab;
        }
        else
        {
            Debug.Log("No such prefab exists for id " + id);
            return null;
        }
    }
    public GameObject GetNodeMemberPrefabById(int id)
    {
        Debug.Log("3");
        if (id >= 0 && id < nodeMemberDetailsList.Count)
        {
            GameObject prefab = (GameObject)Resources.Load(nodeMemberDetailsList[id].prefab);
            Debug.Log(nodeMemberDetailsList[id].prefab);
            Debug.Log(prefab);
            return prefab;
        }
        else
        {
            Debug.Log("No such prefab exists for id " + id);
            return null;
        }
    }
}
[System.Serializable]
public class NodeDetails
{
    public string name;
    public string prefab;
    public string icon;
    public NodeFactory.Category category;

    public NodeDetails(string name, string prefab, string icon, NodeFactory.Category category)
    {
        this.name = name;
        this.prefab = "Prefabs/" + prefab;
        this.icon = "/NodeIcons/" + icon;
        this.category = category;
    }
}

