using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraObject;
    public float speed;
    public float minZoom;
    public float maxZoom;
    public float zoomSensitivity;
    Quaternion cameraRotation;
    Vector3 upVector = Vector3.up;
    Vector3 axis;

    Node.Direction forwardDirection;
    private bool playerExists;

    private void Start() {
        GameController.Game.LevelController.RegisterToLevelCreated(PlayerExists);
    }
    public void ResetCamera() {
        cameraObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        UpdateGravity(Vector3.back, Vector3.up);
    }
    private void Update() {

        
        if (playerExists) {
            cameraObject.transform.position = GameController.Game.CurrentLevel.Player.NodeObjectGraphic.transform.position;
        }
        forwardDirection = GetForwardDirection(cameraObject.transform.forward);
        
    }
    public void RotateAround(float dragDist) {
        cameraObject.transform.RotateAround(cameraObject.transform.position, upVector, dragDist * speed);
        cameraRotation = cameraObject.transform.rotation;
    }
    private void PlayerExists() {
        playerExists = true;
    }

    public void Zoom(float amount) {
        Vector3 cameraPos = Vector3.zero;
        if(Camera.main.orthographic) {
            Camera.main.transform.position = cameraObject.transform.position + (Camera.main.transform.position - cameraObject.transform.position).normalized * maxZoom;
            Camera.main.orthographicSize += amount * zoomSensitivity;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
        else {
            Camera.main.transform.position += (Camera.main.transform.position - cameraObject.transform.position) * amount * 0.5f * zoomSensitivity;
            if((Camera.main.transform.position - cameraObject.transform.position).magnitude > maxZoom){
                Camera.main.transform.position = cameraObject.transform.position + (Camera.main.transform.position - cameraObject.transform.position).normalized * maxZoom;
            }
            if ((Camera.main.transform.position - cameraObject.transform.position).magnitude < minZoom) {
                Camera.main.transform.position = cameraObject.transform.position + (Camera.main.transform.position - cameraObject.transform.position).normalized * minZoom;
            }
           
        }
        
    }
    public void UpdateGravity(Vector3 playerForward, Vector3 playerUp) {
        Vector3 forwardVec = Dir.GetVectorByDirection(forwardDirection);
        bool minus = false;
        bool cross = false;

        if(forwardDirection == Dir.GetDirectionByVector(-playerUp)) {
            cross = true;
            minus = false;
        }
        else {
            cross = false;
            minus = true;
        }
        if (minus == true) {
            playerForward = -playerForward;
        }
        if (cross == true) {
            playerForward = Vector3.Cross(playerForward, playerUp);
        }
        cameraRotation = Quaternion.LookRotation(playerForward, playerUp);
        upVector = playerUp;
        StartCoroutine(GameController.Game.SmoothGraphics.RotateSmoothly(cameraObject.transform, cameraRotation, 0.5f));

    }
    private Node.Direction GetForwardDirection(Vector3 vector) {
        Node.Direction result = Node.Direction.UP;
        float shortestDistance = Mathf.Infinity;
        float distance = 0;
        Vector3 pointOfOrigin = Vector3.zero;
        Vector3 vectorPosition = pointOfOrigin + vector;

        distance = Mathf.Abs(((pointOfOrigin + Vector3.forward) - vector).magnitude);
        if (distance < shortestDistance) {
            shortestDistance = distance;
            result = Node.Direction.FORWARD;
        }
        distance = Mathf.Abs(((pointOfOrigin - Vector3.forward) - vector).magnitude);
        if (distance < shortestDistance) {
            shortestDistance = distance;
            result = Node.Direction.BACK;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.up) - vector).magnitude);
        if (distance < shortestDistance) {
            shortestDistance = distance;
            result = Node.Direction.UP;
        }
        distance = Mathf.Abs(((pointOfOrigin + -Vector3.up) - vector).magnitude);
        if (distance < shortestDistance) {
            shortestDistance = distance;
            result = Node.Direction.DOWN;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.left) - vector).magnitude);
        if (distance < shortestDistance) {
            shortestDistance = distance;
            result = Node.Direction.LEFT;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.right) - vector).magnitude);
        if (distance < shortestDistance) {
            shortestDistance = distance;
            result = Node.Direction.RIGHT;
        }

        return result;
    }
}
