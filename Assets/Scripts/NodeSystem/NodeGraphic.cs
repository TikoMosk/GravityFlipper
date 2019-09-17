using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeGraphic : MonoBehaviour
{
    private Action<Node.Direction> onClick;
    private Node node;

    public Node Node { get => node; set => node = value; }

    public void GetClicked(Node.Direction dir)
    {
        onClick?.Invoke(dir);
    }
    private void Start() {
        GameController.Game.RegisterForGameStateChanged(ColliderUpdate);
        ColliderUpdate();
    }
    public void RegisterToClick(Action<Node.Direction> onClick) {
        this.onClick += onClick;
    }
    private void ColliderUpdate() {
        if (GetComponent<Collider>() != null && !(GameController.Game.CurrentGameState is MenuMode)) {
            Collider col = GetComponent<Collider>();
            if (GameController.Game.CurrentGameState is LevelEditorMode) {
                col.enabled = true;
            }
            else if (node.ColliderActive == false) {
                col.enabled = false;
            }
            else if (node.ColliderActive == true) {
                col.enabled = true;
            }
        }
    }
    private void OnDestroy() {
        GameController.Game.UnRegisterForGameStateChanged(ColliderUpdate);
    }
}
