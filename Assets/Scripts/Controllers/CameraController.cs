using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject swipeIcon;
    public GameObject cameraObject;
    public float speed;
    public float minZoom;
    public float maxZoom;
    public float zoomSensitivity;
    private enum View { Perspective, Orthographic };
    private View cameraStyle;
    Quaternion cameraRotation;
    Vector3 upVector = Vector3.up;
    Vector3 axis;

    Node.Direction forwardDirection;
    private bool playerExists;
    private Action onGravityChanged;

    public Vector3 UpVector { get => upVector; set => upVector = value; }
    public Node.Direction ForwardDirection { get => forwardDirection; }

    public void RegisterToGravityChange(Action onGravityChange)
    {
        onGravityChanged += onGravityChange;
    }
    public void UnRegisterFromGravityChange(Action onGravityChange)
    {
        onGravityChanged -= onGravityChange;
    }
    private void Awake()
    {
        GameController.Game.LevelController.RegisterToLevelCreated(PlayerExists);
    }
    public void ResetCamera()
    {
        if (GameController.Game.CurrentLevel.Player != null) {
            StartCoroutine(ResetCameraToPlayerDirection());
        }
    }
    IEnumerator ResetCameraToPlayerDirection()
    {
        while (GameController.Game.CurrentLevel.Player.created == false)
        {
            yield return null;
        }
        UpdateGravity(Dir.GetVectorByDirection(GameController.Game.CurrentLevel.Player.Facing), Dir.GetVectorByDirection(GameController.Game.CurrentLevel.Player.UpDirection));


    }
    private void Update()
    {

        forwardDirection = GetForwardDirection(cameraObject.transform.forward);
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeCameraStyle();
        }
    }
    public void CameraPositionPlayMode()
    {
        if (playerExists && GameController.Game.CurrentLevel.Player != null && GameController.Game.CurrentLevel.Player.Graphic != null)
        {
            cameraObject.transform.position = GameController.Game.CurrentLevel.Player.Graphic.transform.position;
        }
    }
    public void CameraPositionLEMode(Vector3 inputVector)
    {

        //USE THIS FOR PLAYER MOVEMENT LATER
        //Quaternion rot = Quaternion.LookRotation(Dir.GetVectorByDirection(forwardDirection), upVector);
        //inputVector = rot * inputVector;
        //cameraObject.transform.position += inputVector.normalized * 2 * Time.deltaTime;
        float x = (UpVector.x == 0) ? 1 : 0;
        float y = (UpVector.y == 0) ? 1 : 0;
        float z = (UpVector.z == 0) ? 1 : 0;

        Quaternion rot = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x * x, Camera.main.transform.forward.y * y, Camera.main.transform.forward.z * z), UpVector);
        inputVector = rot * inputVector;
        cameraObject.transform.position += inputVector.normalized * speed * 2 * Time.deltaTime;
    }
    public void RotateAround(float dragDist)
    {
        cameraObject.transform.RotateAround(cameraObject.transform.position, UpVector, dragDist * speed);

    }
    private void PlayerExists()
    {
        playerExists = true;
    }
    public void ChangeCameraStyle()
    {
        if (cameraStyle == View.Orthographic)
        {
            cameraStyle = View.Perspective;
            Camera.main.orthographic = false;
        }
        else
        {
            cameraStyle = View.Orthographic;
            Camera.main.orthographic = true;
            Camera.main.transform.position = cameraObject.transform.position + (Camera.main.transform.position - cameraObject.transform.position).normalized * maxZoom;
        }
    }

    public void Zoom(float amount)
    {
        Vector3 cameraPos = Vector3.zero;
        if (Camera.main.orthographic)
        {
            Camera.main.transform.position = cameraObject.transform.position + (Camera.main.transform.position - cameraObject.transform.position).normalized * maxZoom;
            Camera.main.orthographicSize += amount * zoomSensitivity;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
        else
        {
            Camera.main.transform.position += (Camera.main.transform.position - cameraObject.transform.position) * amount * 0.5f * zoomSensitivity;
            if ((Camera.main.transform.position - cameraObject.transform.position).magnitude > maxZoom)
            {
                Camera.main.transform.position = cameraObject.transform.position + (Camera.main.transform.position - cameraObject.transform.position).normalized * maxZoom;
            }
            if ((Camera.main.transform.position - cameraObject.transform.position).magnitude < minZoom)
            {
                Camera.main.transform.position = cameraObject.transform.position + (Camera.main.transform.position - cameraObject.transform.position).normalized * minZoom;
            }

        }

    }
    public void UpdateGravity(Vector3 playerForward, Vector3 playerUp)
    {
        Vector3 forwardVec = Dir.GetVectorByDirection(ForwardDirection);
        bool minus = false;
        bool cross = false;

        if (ForwardDirection == Dir.GetDirectionByVector(-playerUp))
        {
            cross = true;
            minus = false;
        }
        else
        {
            cross = false;
            minus = true;
        }
        if (minus == true)
        {
            playerForward = -playerForward;
        }
        if (cross == true)
        {
            playerForward = Vector3.Cross(playerForward, playerUp);
        }
        cameraRotation = Quaternion.LookRotation(playerForward, playerUp);
        UpVector = playerUp;
        if (onGravityChanged != null)
        {
            onGravityChanged.Invoke();
        }
        StartCoroutine(GameController.Game.SmoothGraphics.RotateSmoothly(cameraObject.transform, cameraRotation, 1f));


    }
    private Node.Direction GetForwardDirection(Vector3 vector)
    {
        Node.Direction result = Node.Direction.UP;
        float shortestDistance = Mathf.Infinity;
        float distance = 0;
        Vector3 pointOfOrigin = Vector3.zero;
        Vector3 vectorPosition = pointOfOrigin + vector;

        distance = Mathf.Abs(((pointOfOrigin + Vector3.forward) - vector).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = Node.Direction.FORWARD;
        }
        distance = Mathf.Abs(((pointOfOrigin - Vector3.forward) - vector).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = Node.Direction.BACK;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.up) - vector).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = Node.Direction.UP;
        }
        distance = Mathf.Abs(((pointOfOrigin + -Vector3.up) - vector).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = Node.Direction.DOWN;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.left) - vector).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = Node.Direction.LEFT;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.right) - vector).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = Node.Direction.RIGHT;
        }

        return result;
    }
}
