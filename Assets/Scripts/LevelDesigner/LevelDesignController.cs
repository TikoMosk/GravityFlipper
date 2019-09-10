using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignController : MonoBehaviour {
    public GameObject levelEditorPanel;
    public GameObject playModePanel;
    public GameObject placeOptionsPanel;
    public GameObject rotateOptionsPanel;
    public GameObject nodePickerWindow;
    public GameObject rotateGizmoPrefab;
    private GameObject rotateGizmo;
    public GameObject moveGizmoPrefab;
    private GameObject moveGizmo;
    public Canvas worldSpaceCanvas;
    int blockId = 1;
    private bool placeNodeMember;
    private bool isNodeMember;
    private int rotateVertical;
    Quaternion rotation;
    Node selectedNode;


    enum Tool { None, Place, Remove, Move, Rotate };
    private Tool tool;

    public int BlockId { get => blockId; set => blockId = value; }
    public bool PlaceNodeMember { get => placeNodeMember; set => placeNodeMember = value; }

    private void Start() {
        GameController.Game.LevelController.RegisterToLevelCreated(DeleteGizmos);
    }
    public void SetTool(int toolNumber) {
        if (tool == (Tool)toolNumber) {
            tool = Tool.None;
        }
        else {
            tool = (Tool)toolNumber;
        }
        if (tool != Tool.Move) {
            if (moveGizmo != null) {
                Destroy(moveGizmo);
            }
        }
        if (tool != Tool.Rotate) {
            if (rotateGizmo != null) {
                Destroy(rotateGizmo);
            }
        }
        

        ToggleToolOptions();
    }
    public void OnNodeClick(Node n, Node.Direction dir) {
        if (tool == Tool.Place) {
            Node placeNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir);
            if (placeNode != null) {
                if (placeNodeMember) {

                    GameController.Game.LevelController.Level.AddNodeMember(placeNode.X, placeNode.Y, placeNode.Z, blockId, Node.Direction.FORWARD, Node.Direction.UP);
                    GameController.Game.LevelController.CreateNodeMemberGraphic(placeNode.X, placeNode.Y, placeNode.Z);
                }
                else {
                    GameController.Game.LevelController.Level.SetNode(placeNode.X, placeNode.Y, placeNode.Z, blockId);
                }

            }
        }
        if(n.NodeMember != null && n.NodeMember.Id != 0) {
            isNodeMember = true;
        }
        else {
            isNodeMember = false;
        }
        if (tool == Tool.Remove) {
            n.SetNodeType(0);
            n.DestroyNodeMember();
        }
        if (tool == Tool.Move) {
            selectedNode = n;
            if (moveGizmo == null) {
                moveGizmo = Instantiate(moveGizmoPrefab, n.GetPosition(), Quaternion.identity);
                moveGizmo.transform.SetParent(worldSpaceCanvas.transform);
            }
            else {
                moveGizmo.transform.position = n.GetPosition();
            }

        }
        if (tool == Tool.Rotate) {
            selectedNode = n;
            if (rotateGizmo == null) {
                rotateGizmo = Instantiate(rotateGizmoPrefab, n.GetPosition(), Quaternion.identity);
                rotateGizmo.transform.SetParent(worldSpaceCanvas.transform);
            }
            else {
                rotateGizmo.transform.position = n.GetPosition();
            }

        }
        


    }
    public void RotateBlock(bool plus) {
        int degrees = 90;
        if(!plus) {
            degrees = -90;
        }
        if(isNodeMember) {
            NodeMember sNodeMember = selectedNode.NodeMember;
            sNodeMember.SetRotation(Dir.GetDirectionByVector(Quaternion.Euler(0, degrees, 0) * Dir.GetVectorByDirection(sNodeMember.Facing)),sNodeMember.UpDirection);
            
        }
        else {
            selectedNode.SetRotation(Dir.GetDirectionByVector(Quaternion.Euler(0, degrees, 0) * Dir.GetVectorByDirection(selectedNode.Facing)), selectedNode.UpDirection);
        }
    }
    public void MoveBlock(Vector3 dir) {
        Node destNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(selectedNode,Dir.GetDirectionByVector(dir));
        if(destNode == null) {
            return;
        }
        if (isNodeMember) {
            NodeMember sNodeMember = selectedNode.NodeMember;
            GameController.Game.CurrentLevel.MoveObject(selectedNode, destNode);

        }
        else {
            /*Node a = selectedNode;
            selectedNode.SetNodeType(0);*/

        }
        selectedNode = destNode;
        moveGizmo.transform.position = selectedNode.GetPosition();
    }
    public void ChangeRotationStyle(int style) {
        if (rotateVertical != style) {
            rotateVertical = style;

        }

    }
    private void Update() {
        if (rotateGizmo != null) {
            if (rotateVertical == 1) {
                rotateGizmo.transform.rotation = Quaternion.LookRotation(GameController.Game.CameraController.UpVector, new Vector3(Mathf.Round(Camera.main.transform.forward.x), Mathf.Round(Camera.main.transform.forward.y), Mathf.Round(Camera.main.transform.forward.z)));
            }
            else {
                rotateGizmo.transform.rotation = Quaternion.LookRotation(Dir.GetVectorByDirection(GameController.Game.CameraController.ForwardDirection), GameController.Game.CameraController.UpVector);
            }
        }
    }
    public void ChangeMode(int mode) {
        if (mode == 0) {
            levelEditorPanel.SetActive(true);
            playModePanel.SetActive(false);
            GameController.Game.ChangeGameState("LevelEditorMode");
        }
        if (mode == 1) {
            levelEditorPanel.SetActive(false);
            playModePanel.SetActive(true);
            GameController.Game.ChangeGameState("TestMode");
        }
        if (mode != 0) {
            DeleteGizmos();
        }

    }
    private void DeleteGizmos() {
        if (rotateGizmo != null) {
            Destroy(rotateGizmo);
        }
        if (moveGizmo != null) {
            Destroy(moveGizmo);
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
