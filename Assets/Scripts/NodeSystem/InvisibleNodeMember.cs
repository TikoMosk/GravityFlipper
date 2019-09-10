using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvisibleNodeMember : MonoBehaviour {
    Node n;
    public GameObject iconPrefab;
    public Sprite iconSprite;
    private GameObject canv;
    private GameObject icon;
    private void Start() {
        if (GameObject.Find("WorldSpaceCanvas") != null) {
            canv = GameObject.Find("WorldSpaceCanvas");
            icon = Instantiate(iconPrefab, canv.transform);
            icon.GetComponent<Image>().sprite = iconSprite;

            icon.SetActive(true);
        }
    }
    private void OnDestroy() {
        Destroy(icon);
    }
    private void Update() {
        if (icon != null) {
            if (GetComponent<NodeMemberGraphic>() != null) {
                n = GetComponent<NodeMemberGraphic>().Node;
                icon.transform.position = n.GetPosition();
                icon.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, GameController.Game.CameraController.UpVector);
            }
            else {
                n = GetComponent<NodeGraphic>().Node;
                icon.transform.position = n.GetPosition();
                icon.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, GameController.Game.CameraController.UpVector);
            }
        }

    }
}
