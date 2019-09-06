using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NodeFactory : MonoBehaviour{

    List<NodeDetails> nodeDetailsList;
    List<NodeDetails> nodeMemberDetailsList;


    private static NodeFactory factory;

    public static NodeFactory Factory { get => factory; set => factory = value; }
    [System.Serializable]
    public enum Category { DecorativeBlock, Living, FunctioningBlock, Logic, Miscellaneous , Technical};
    private void Awake() {
        if(factory != null && factory != this) {
            Destroy(this);
        }
        else {
            factory = this;
        }
        nodeDetailsList = new List<NodeDetails>();
        nodeMemberDetailsList = new List<NodeDetails>();
        CreateNodeDetails();
    }
    private void CreateNodeDetails() {

        //NODES
        nodeDetailsList.Add(new NodeDetails(
            0,
            false,
            "Air",
            "",
            "",
            "",
            Category.Technical
            ));
        nodeDetailsList.Add(new NodeDetails(
            1,
            false,
            "Block", 
            "This is a block that the player can walk on",
            "Block", 
            "Block", 
            Category.DecorativeBlock
            ));
        nodeDetailsList.Add(new NodeDetails(
            2,
            false,
            "Laser",
            "",
            "LaserCube",
            "",
            Category.FunctioningBlock
            ));
        nodeDetailsList.Add(new NodeDetails(
            3,
            false,
            "Win Node",
            "Place this, wherever you want the player to reach to pass the level",
            "WinBlock",
            "winIcon",
            Category.Miscellaneous
            ));
        nodeDetailsList.Add(new NodeDetails(
            4,
            false,
            "Weak Block",
            "",
            "Weak Block",
            "",
            Category.FunctioningBlock
            ));
        nodeDetailsList.Add(new NodeDetails(
            5,
            false,
            "Spike",
            "",
            "Spike",
            "",
            Category.FunctioningBlock
            ));

        //NODEMEMBERS
        nodeMemberDetailsList.Add(new NodeDetails(
            0,
            true,
            "Empty",
            "",
            "",
            "",
            Category.Technical
            ));
        nodeMemberDetailsList.Add(new NodeDetails(
            1,
            true,
            "Player",
            "",
            "Player",
            "Player",
            Category.Living
            ));
        nodeMemberDetailsList.Add(new NodeDetails(
            2,
            true,
            "Enemy",
            "",
            "EnemySpider",
            "",
            Category.Living
            ));
        nodeMemberDetailsList.Add(new NodeDetails(
           3,
           true,
           "Patrol Enemy",
           "",
           "EnemySpiderPatrol",
           "",
           Category.Living
           ));
        nodeMemberDetailsList.Add(new NodeDetails(
            4,
            true,
            "Light",
            "",
            "PointLight",
            "",
            Category.Miscellaneous
            ));
        nodeMemberDetailsList.Add(new NodeDetails(
           5,
           true,
           "Moving Laser",
           "",
           "LaserCubeMoving",
           "",
           Category.FunctioningBlock
           ));

    }

    public List<NodeDetails> GetNodeDetailsByCategory(Category category) {
        var categorizedNodeList = nodeDetailsList.Where(x => x.category == category);
        var categorizedNodeMemberList = nodeMemberDetailsList.Where(x => x.category == category);
        List<NodeDetails> returnList = categorizedNodeList.Union(categorizedNodeMemberList).ToList<NodeDetails>();
        return returnList;
    }

    public GameObject GetNodePrefabById(int id) {
        if (id >= 0 && id < nodeDetailsList.Count) {
            GameObject prefab = (GameObject)Resources.Load(nodeDetailsList[id].prefab);
            return prefab;
        }
        else {
            Debug.Log("No such prefab exists for id " + id);
            return null;
        }
    }
    public GameObject GetNodeMemberPrefabById(int id) {
        if (id >= 0 && id < nodeMemberDetailsList.Count) {
            GameObject prefab = (GameObject)Resources.Load(nodeMemberDetailsList[id].prefab);
            return prefab;
        }
        else {
            Debug.Log("No such prefab exists for id " + id);
            return null;
        }
    }
}
[System.Serializable]
public class NodeDetails {
    public int id;
    public bool nodeMember;
    public string name;
    public string description;
    public string prefab;
    public string icon;
    public NodeFactory.Category category;
    

    public NodeDetails(int id, bool nodeMember,string name, string description, string prefab, string icon, NodeFactory.Category category) {
        this.id = id;
        this.nodeMember = nodeMember;
        this.name = name;
        this.description = description;
        this.prefab = "Prefabs/" + prefab;
        this.icon = "NodeIcons/" + icon;
        this.category = category;
        
    }
}
   
