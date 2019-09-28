﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinNodeIcon : MonoBehaviour
{
    public Node currentNode;
    public GameObject icon;
    GameObject worldSpaceCanvas;

    private void Start() {
        currentNode = GameController.Game.CurrentLevel.GetNode(transform.position);
        worldSpaceCanvas = GameObject.Find("WorldCanvas");
        if(worldSpaceCanvas == null) {
            worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas");
        }
        if (icon != null && worldSpaceCanvas != null)
        {
            icon.transform.parent = worldSpaceCanvas.transform;
            if (currentNode != null)
            {
                Debug.Log(currentNode.GetPosition());
                icon.transform.position = currentNode.GetPosition();
            }
        }
    }


}