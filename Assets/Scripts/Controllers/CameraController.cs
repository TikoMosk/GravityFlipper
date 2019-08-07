using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraObject;
    Quaternion cameraRotation;
    Vector2 dragStartPos;
    Vector2 dragEndPos;
    Vector3 upVector = Vector3.up;
    Vector3 axis;
    public float speed;
    Node.Direction forwardDirection;


    private void Update() {
        
        if(Input.GetMouseButtonDown(0)) {
            dragStartPos = Input.mousePosition;

        }
        if (Input.GetMouseButton(0)) {
            dragEndPos = Input.mousePosition;
            if(Vector2.Distance(dragEndPos,dragStartPos) > 30) {
                cameraObject.transform.RotateAround(cameraObject.transform.position, upVector, Input.GetAxis("Mouse X") * speed);
                cameraRotation = cameraObject.transform.rotation;
            }
        }
        cameraObject.transform.rotation = Quaternion.Lerp(cameraObject.transform.rotation, cameraRotation, 0.1f);
        forwardDirection = GetForwardDirection(cameraObject.transform.forward);
        Debug.Log(forwardDirection);
    }
    public void UpdateCamera(Vector3 playerForward, Vector3 playerUp) {
        cameraRotation = Quaternion.LookRotation(playerForward, playerUp);
        /*Vector3 forwardVec = Dir.GetVectorByDirection(forwardDirection);
        //cameraRotation = Quaternion.Euler( Vector3.Cross(playerUp, Vector3.up) * -90);
        if (forwardDirection == Node.Direction.FORWARD || forwardDirection == Node.Direction.RIGHT) {
            cameraRotation = Quaternion.LookRotation(-playerForward, playerUp);
            //cameraRotation = cameraRotation + Quaternion.Euler(Vector3.up * -90);
        }
        if (forwardDirection == Node.Direction.FORWARD || forwardDirection == Node.Direction.BACK) {
            //cameraRotation = Quaternion.LookRotation(Vector3.Cross(playerForward, playerUp) * 180, playerUp);
            
            //cameraRotation = cameraRotation + Quaternion.Euler(Vector3.up * -90);
        }
        */

        upVector = playerUp;

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
