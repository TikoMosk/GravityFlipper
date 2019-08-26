using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignController : MonoBehaviour
{
    public GameObject levelEditorPanel;
    public GameObject playModePanel;
    public GameObject placeOptionsPanel;
    public GameObject rotateOptionsPanel;
    public GameObject nodePickerWindow;
    int blockId = 1;
    private bool placeNodeMember;


    enum Tool { None, Place, Remove, Move, Rotate };
    private Tool tool;

    public int BlockId { get => blockId; set => blockId = value; }
    public bool PlaceNodeMember { get => placeNodeMember; set => placeNodeMember = value; }

    public void SetTool(int toolNumber) {
        if(tool == (Tool) toolNumber) {
            tool = Tool.None;
        }
        else {
            tool = (Tool)toolNumber;
        }
        ToggleToolOptions();
    }
    public void OnNodeClick(Node n, Node.Direction dir)
    {
        if(tool == Tool.Place)
        {
            Node placeNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir);
            if(placeNode != null) {
                if (placeNodeMember) {
                    
                    GameController.Game.LevelController.Level.AddNodeMember(placeNode.X, placeNode.Y, placeNode.Z, blockId,Node.Direction.FORWARD,Node.Direction.UP);
                    GameController.Game.LevelController.CreateNodeMemberGraphic(placeNode.X, placeNode.Y, placeNode.Z);
                }
                else {
                    GameController.Game.LevelController.Level.SetNode(placeNode.X, placeNode.Y, placeNode.Z, blockId);
                }
                
            }
            
        }
        if (tool == Tool.Remove)
        {
            n.SetNodeType(0);
            n.DestroyNodeMember();
        }
        
    }
    public void ChangeMode(int mode) {
        if(mode == 0) {
            levelEditorPanel.SetActive(true);
            playModePanel.SetActive(false);
            GameController.Game.ChangeGameState("LevelEditorMode");
        }
        if (mode == 1) {
            levelEditorPanel.SetActive(false);
            playModePanel.SetActive(true);
            GameController.Game.ChangeGameState("TestMode");
        }

    }
    public void ToggleToolOptions() {
        placeOptionsPanel.SetActive(tool == Tool.Place);
        rotateOptionsPanel.SetActive(tool == Tool.Rotate);
    }
    public void ToggleNodePicker() {
        nodePickerWindow.SetActive(!nodePickerWindow.activeSelf);
    }
}
