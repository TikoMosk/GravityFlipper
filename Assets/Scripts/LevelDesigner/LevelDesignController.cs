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
    enum Tool { Place, Remove, Rotate };
    private Tool tool;

    public void SetTool(int toolNumber) {
        tool = (Tool)toolNumber;
    }
    public void OnNodeClick(Node n, Node.Direction dir)
    {
        if(tool == Tool.Place)
        {
            Node placeNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir);
            GameController.Game.LevelController.Level.SetNode(placeNode.X, placeNode.Y, placeNode.Z, 1);
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
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }
    public void ToggleNodePicker() {
        nodePickerWindow.SetActive(!nodePickerWindow.activeSelf);
    }
}
