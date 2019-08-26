using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodePicker : MonoBehaviour
{
    public GameObject panel;
    public GameObject nodeCellUI;
    public void ClearPanel() {
        for (int i = 0; i < panel.transform.childCount; i++) {
            Destroy(panel.transform.GetChild(i).gameObject);
        }
    }
    public void PopulatePanelByCategory(int category) {
        ClearPanel();
        Debug.Log("POPULATE " + category);
        List<NodeDetails> categorizedList = NodeFactory.Factory.GetNodeDetailsByCategory((NodeFactory.Category)category);
        foreach (NodeDetails details in categorizedList) {
            GameObject cell = Instantiate(nodeCellUI);
            cell.transform.SetParent(panel.transform, false);
            var spr = Resources.Load<Sprite>(details.icon);
            if (spr == null) {
                spr = Resources.Load<Sprite>(details.icon + "Undefined");
            }
            cell.GetComponent<NodePickerCell>().SetUp(details.id,details.nodeMember, details.name, details.description, spr);
        }
    }
}
