using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignController : MonoBehaviour
{
    public GameObject levelEditorPanel;
    public GameObject playModePanel;
    public GameObject optionsPanel;
    public GameObject nodePickerWindow;
    enum Tool { None, Place, Remove, Move, Rotate };
    private Tool tool;

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
                GameController.Game.LevelController.Level.SetNode(placeNode.X, placeNode.Y, placeNode.Z, 1);
            }
            
        }
        if (tool == Tool.Remove)
        {
            n.SetNodeType(0);
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
            GameController.Game.ChangeGameState("PlayMode");
        }

    }
    public void ToggleToolOptions() {
        optionsPanel.SetActive(tool == Tool.Place);
    }
    public void ToggleNodePicker() {
        nodePickerWindow.SetActive(!nodePickerWindow.activeSelf);
    }
}
