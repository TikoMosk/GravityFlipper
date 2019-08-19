using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodePickerCell : MonoBehaviour
{
    int id;
    bool nodeMember;
    public Text nameText;
    public Text descText;
    public Image image;

    public void SetUp( int id, bool nodeMember,string name, string description, Sprite icon) {
        this.id = id;
        this.nodeMember = nodeMember;
        nameText.text = name;
        descText.text = description;
        image.sprite = icon;
    }
    public void Click() {
        GameController.Game.LevelDesignController.PlaceNodeMember = nodeMember;
        GameController.Game.LevelDesignController.BlockId = id;
       
    }
}
