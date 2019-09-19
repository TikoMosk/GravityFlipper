using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignController : MonoBehaviour {
    public GameObject levelEditorPanel;
    public GameObject playModePanel;
    public GameObject placeOptionsPanel;
    public GameObject rotateOptionsPanel;
    public GameObject connectorOptionsPanel;
    public GameObject nodePickerWindow;
    public GameObject rotateGizmoPrefab;
    public GameObject connectorToggleIcon;
    public GameObject connectorReceiverIcon;
    public Text connectToolText;
    private GameObject rotateGizmo;
    public GameObject moveGizmoPrefab;
    private GameObject moveGizmo;
    public Canvas worldSpaceCanvas;
    int blockId = 1;
    private bool placeNodeMember;
    private bool connectState;
    private bool isNodeMember;
    private int rotateVertical;
    Quaternion rotation;
    Node selectedNode;
    Node connectNode;
    private List<GameObject> worldCanvasObjects;
    private NodeToggler toggler;

    enum Tool { None, Place, Remove, Move, Rotate, Connector };
    private Tool tool;
    private bool selectState;

    public int BlockId { get => blockId; set => blockId = value; }
    public bool PlaceNodeMember { get => placeNodeMember; set => placeNodeMember = value; }

    private void Start() {
        GameController.Game.LevelController.RegisterToLevelCreated(DeleteGizmos);
        worldCanvasObjects = new List<GameObject>();
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
        if (tool == Tool.Connector) {
            connectState = false;
            foreach (GameObject g in worldCanvasObjects) {
                g.SetActive(true);
            }
        }
        if (tool != Tool.Connector) {
            foreach (GameObject g in worldCanvasObjects) {
                g.SetActive(false);
            }
        }

        ToggleToolOptions();
    }
    public void OnNodeClick(Node n, Node.Direction dir) {
        if (tool == Tool.Place) {
            Node placeNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir);
            if (placeNode != null && placeNode.Id == 0 && placeNode.NodeMember == null) {
                if (placeNodeMember) {

                    GameController.Game.LevelController.Level.AddNodeMember(placeNode.X, placeNode.Y, placeNode.Z, blockId, Node.Direction.FORWARD, Node.Direction.UP);
                    GameController.Game.LevelController.CreateNodeMemberGraphic(placeNode.X, placeNode.Y, placeNode.Z);
                }
                else {
                    GameController.Game.LevelController.Level.SetNode(placeNode.X, placeNode.Y, placeNode.Z, blockId);
                }

            }
        }
        if (n.NodeMember != null && n.NodeMember.Id != 0) {
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
        if (tool == Tool.Connector) {
            GameObject c = GameObject.Find("WorldSpaceCanvas");
            if(connectState == false) {
                
                if (n.NodeMember != null && n.NodeMember.NodeObjectGraphic.GetComponent<NodeToggler>() != null) {
                    toggler = n.NodeMember.NodeObjectGraphic.GetComponent<NodeToggler>();
                    connectState = true;
                    Debug.Log("1");
                }
                else if(n.NodeGraphic != null && n.NodeGraphic.GetComponent<NodeToggler>() != null){
                    toggler = n.NodeGraphic.GetComponent<NodeToggler>();
                    connectState = true;
                    
                }
            }
            else {
                if (toggler != null) {
                    if (toggler.CheckIfSameConnectedNode(n)) {
                        toggler.ConnectNode(n);
                        GameObject ic1 = Instantiate(connectorToggleIcon, c.transform);
                        GameObject ic2 = Instantiate(connectorReceiverIcon, c.transform);
                        Material mat = Instantiate(ic1.GetComponent<Image>().material);
                        Color32 col = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                        mat.SetColor("_MainColor", col);
                        ic1.transform.position = toggler.GetPos(); 
                        ic2.transform.position = toggler.GetConnectNodePosition();
                        ic1.GetComponent<Image>().material = mat;
                        ic2.GetComponent<Image>().material = mat;
                        connectState = false;
                        worldCanvasObjects.Add(ic1);
                        worldCanvasObjects.Add(ic2);
                    }
                    else {
                        connectState = false;
                    }

                }
                
            } 
            if(connectState == false) {
                connectToolText.text = "Select the node that is your toggler";
            }
            else {
                connectToolText.text = "Select the node that is going to get triggered";
            }
        }
    }
    public void RotateBlock(bool plus) {
        int degrees = 90;
        if (!plus) {
            degrees = -90;
        }
        if (isNodeMember) {
            NodeMember sNodeMember = selectedNode.NodeMember;
            sNodeMember.SetRotation(Dir.GetDirectionByVector(Quaternion.Euler(0, degrees, 0) * Dir.GetVectorByDirection(sNodeMember.Facing)), sNodeMember.UpDirection);

        }
        else {
            selectedNode.SetRotation(Dir.GetDirectionByVector(Quaternion.Euler(0, degrees, 0) * Dir.GetVectorByDirection(selectedNode.Facing)), selectedNode.UpDirection);
        }
    }
    public void MoveBlock(Vector3 dir) {
        Node destNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(selectedNode, Dir.GetDirectionByVector(dir));
        if (destNode == null) {
            return;
        }
        if (isNodeMember) {
            if(destNode.Id == 0 && selectedNode.Id == 0) {
                NodeMember sNodeMember = selectedNode.NodeMember;
                sNodeMember.LocationNode = destNode;
                GameController.Game.CurrentLevel.MoveMemberNoAnimation(selectedNode, destNode);
                selectedNode = destNode;
                moveGizmo.transform.position = selectedNode.GetPosition();
            }

        }
        else {
            if(destNode.NodeMember == null) {
                int selectedNodeId = selectedNode.Id;
                int destNodeId = destNode.Id;
                selectedNode.SetNodeType(0);
                destNode.SetNodeType(0);
                selectedNode.SetNodeType(destNodeId);
                destNode.SetNodeType(selectedNodeId);
                selectedNode = destNode;
                moveGizmo.transform.position = selectedNode.GetPosition();
            }
        }
        
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
            foreach (GameObject g in worldCanvasObjects) {
                g.SetActive(true);
            }
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
        foreach(GameObject g in worldCanvasObjects) {
            g.SetActive(false);
        }
        if (rotateGizmo != null) {
            Destroy(rotateGizmo);
        }
        if (moveGizmo != null) {
            Debug.Log(moveGizmo);
            Destroy(moveGizmo);
        }
    }
    public void ToggleToolOptions() {
        placeOptionsPanel.SetActive(tool == Tool.Place);
        rotateOptionsPanel.SetActive(tool == Tool.Rotate);
        connectorOptionsPanel.SetActive(tool == Tool.Connector);
    }
    public void ToggleNodePicker() {
        nodePickerWindow.SetActive(!nodePickerWindow.activeSelf);
    }
}
