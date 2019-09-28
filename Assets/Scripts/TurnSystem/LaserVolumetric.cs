using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserVolumetric : MonoBehaviour {
    bool isShooting;
    public bool startTurnedOn;
    public bool toggleSwitchesState;
    public bool toggleRotates;
    public Vector3 endPos;
    private Vector3 dir;
    private VolumetricLineBehavior vl;
    private float laserLength = 500f;

    private void Awake() {
        vl = GetComponentInChildren<VolumetricLineBehavior>();

    }
    private void Start() {
        if (GetComponent<NodeToggleReceiver>() != null && toggleSwitchesState) {
            GetComponent<NodeToggleReceiver>().RegisterToToggleReceiver(SwitchLaser);
        }
        else if (GetComponent<NodeToggleReceiver>() != null && toggleRotates) {
            GetComponent<NodeToggleReceiver>().RegisterToToggleReceiver(RotateLaser);
        }
        if (!startTurnedOn) {
            vl.EndPos = Vector3.zero;
        }
        else {
            isShooting = true;
        }

    }
    private void OnDestroy() {
        if (GetComponent<NodeToggleReceiver>() != null && toggleSwitchesState) {
            GetComponent<NodeToggleReceiver>().UnRegisterToToggleReceiver(SwitchLaser);
        }
        else if (GetComponent<NodeToggleReceiver>() != null && toggleRotates) {
            GetComponent<NodeToggleReceiver>().UnRegisterToToggleReceiver(RotateLaser);
        }
    }
    public void SwitchLaser() {
        isShooting = !isShooting;
        if (!isShooting) {
            vl.gameObject.SetActive(false);
        }
        else {
            vl.gameObject.SetActive(true);
        }
    }
    public void RotateLaser() {
        Node n = GameController.Game.CurrentLevel.GetNode(transform.position);
        Vector3 currentDir = Dir.GetVectorByDirection(n.Facing);
        Quaternion rot = Quaternion.AngleAxis(90, Dir.GetVectorByDirection(n.UpDirection));
        Vector3 changedDir = rot * currentDir;
        n.SetRotation(Dir.GetDirectionByVector(changedDir), n.UpDirection);
    }

    //todo
    private void FixedUpdate() {
        if (isShooting) {
            laserLength = 100;
            if (GetComponent<NodeMemberGraphic>() != null) {
                var graphic = GetComponent<NodeMemberGraphic>();
                dir = Dir.GetVectorByDirection(graphic.Node.NodeMember.Facing);
            }
            else if (GetComponent<NodeGraphic>() != null) {
                var graphic = GetComponent<NodeGraphic>();
                dir = Dir.GetVectorByDirection(graphic.Node.Facing);
            }
            if (GameController.Game.SmoothGraphics.AnimationCount == 0) {
                RaycastHit hit;
                vl.EndPos = transform.InverseTransformPoint(transform.position + dir) * laserLength;
                if (Physics.Raycast(transform.position, dir, out hit, laserLength)) {
                    vl.EndPos = transform.InverseTransformPoint(hit.point); ;
                    if (hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>() != null && hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember != null) {
                        if (GameController.Game.LevelController.Factory.isLiving(hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember.Id)) {
                            if (hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember.Id == 1) {
                                hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().Node.NodeMember.Destroy();
                            }
                            else {
                                Destroy(hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().gameObject);
                                GameController.Game.CurrentLevel.GetNode(hit.collider.gameObject.GetComponentInParent<NodeMemberGraphic>().gameObject.transform.position).NodeMember = null;
                            }
                        }

                    }
                }

                Debug.DrawRay(transform.position, dir, Color.blue);
            }
        }
    }


}
