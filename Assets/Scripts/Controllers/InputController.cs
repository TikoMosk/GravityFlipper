﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour {
    Vector3 touchStart;
    Vector3 touchEnd;
    Touch touchZero;
    Touch touchOne;
    Vector2 touchZeroPrevPos;
    Vector2 touchOnePrevPos;
    Vector3 inputPosDelta;
    Vector3 inputPreviousPos;
    bool isDragging;
    public LayerMask ignoreRaycastMask;
    float tracker = 0;

    private void Update() {
        
        if (GameController.Game.SmoothGraphics.AnimationCount == 0) {
           
            if (Input.touchCount > 0) {
                if (EventSystem.current.IsPointerOverGameObject()) {

                    return;

                }
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    touchStart = Input.GetTouch(0).position;
                }

                if (Input.touchCount == 2) {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                    float difference = prevMagnitude - currentMagnitude;

                    GameController.Game.CameraController.Zoom(difference * 0.005f);
                    isDragging = true;
                }
                else if (Input.touchCount == 1) {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                        inputPosDelta = Input.GetTouch(0).deltaPosition;
                        GameController.Game.CameraController.RotateAround(inputPosDelta.x * 0.025f);
                        isDragging = true;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                        if (!isDragging) {
                            Click();

                        }
                        isDragging = false;
                    }
                }

            }
            else {
                if (EventSystem.current.IsPointerOverGameObject()) {

                    return;

                }
                if (Input.GetMouseButton(0)) {
                    inputPosDelta = Input.mousePosition - inputPreviousPos;
                    if (inputPosDelta.magnitude > 7) {
                        GameController.Game.CameraController.RotateAround(inputPosDelta.x * 0.025f);
                        isDragging = true;
                    }
                }
                else if (Input.GetMouseButtonUp(0)) {
                    if (!isDragging) {
                        Click();

                    }
                    isDragging = false;

                }

                if (Input.GetAxis("Mouse ScrollWheel") != 0) {

                    GameController.Game.CameraController.Zoom(-Input.GetAxis("Mouse ScrollWheel") * 5);
                }
                inputPreviousPos = Input.mousePosition;
            }
        }


    }
    private void Click() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.GetComponent<NodeGraphic>() != null) {
                hit.collider.gameObject.GetComponent<NodeGraphic>().GetClicked(Dir.GetDirectionByVector(hit.normal));
            }

        }

    }


}
