using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class NodeFactory : MonoBehaviour {

    public List<NodeDetails> nodeDetailsList;
    public List<NodeDetails> nodeMemberDetailsList;

    [System.Serializable]
    public enum Category { DecorativeBlock, Living, FunctioningBlock, Logic, Miscellaneous, Technical };
    public List<NodeDetails> GetNodeDetailsByCategory(Category category) {
        
        var categorizedNodeList = nodeDetailsList.Where(x => x.category == category);
        var categorizedNodeMemberList = nodeMemberDetailsList.Where(x => x.category == category);
        List<NodeDetails> returnList = categorizedNodeList.Union(categorizedNodeMemberList).ToList<NodeDetails>();
        return returnList;
    }

    public NodeDetails GetNodeDetailsById(int id, bool nodeMember) {
        if (nodeMember) {
            return nodeMemberDetailsList[id];
        }
        else {
            return nodeDetailsList[id];
        }
    }

    public GameObject GetNodePrefabById(int id) {
        if (id >= 0 && id < nodeDetailsList.Count) {
            GameObject prefab = nodeDetailsList[id].prefab;
            return prefab;
        }
        else {
            Debug.Log("No such prefab exists for id " + id);
            return null;
        }
    }
    public GameObject GetNodeMemberPrefabById(int id) {
        if (id >= 0 && id < nodeMemberDetailsList.Count) {
            GameObject prefab = nodeMemberDetailsList[id].prefab;
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
    public bool notSolid;
    public string name;
    public string description;
    public GameObject prefab;
    public Sprite icon;
    public NodeFactory.Category category;


}

