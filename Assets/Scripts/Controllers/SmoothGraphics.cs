using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmoothGraphics : MonoBehaviour
{
    public GameObject worldParent;
    public Camera mainCam;
    public GameObject cameraObject;
    public float rotateTime;

    private Vector3 startPos;
    private bool isRotatingSmoothly;
    public bool isMovingSmoothly;
    private bool isDragging;
    private Node playerNode;

    private void Awake()
    {
        startPos = mainCam.transform.position;
    }
    private void Update()
    {
        if(playerNode != null)
        {
            playerNode = GameController.Game.LevelController.PlayerNode;
            //MoveSmoothly(cameraObject.transform, cameraObject.transform.position, playerNode.NodeObject.NodeObjectGraphic.transform.position, rotateTime);
            cameraObject.transform.position = playerNode.NodeObject.NodeObjectGraphic.transform.position;
        }
        
    }
    /*public void Rotate(Transform transformToRotate, Vector3 point, Vector3 axis, float angle, float secondsToComplete)
    {
        bool isRotating = false;
        if(!isRotating)
        {
            StartCoroutine(RotateSmoothly(transformToRotate, point, axis, angle, secondsToComplete));
        }
    }*/

    public void RotateWorld(Vector3 axis)
    {
        Vector3 worldCenter = new Vector3(GameController.Game.CurrentLevel.Width / 2, GameController.Game.CurrentLevel.Height / 2, GameController.Game.CurrentLevel.Length / 2);
        GameController.Game.LevelController.transform.position = Vector3.zero;
        StartCoroutine(RotateSmoothly(worldParent.transform, Vector3.zero,axis,-90,rotateTime));
    }
    public void MovePlayer(Transform playerTransform, Vector3 start, Vector3 dest)
    {
        StartCoroutine(MoveSmoothly(playerTransform, playerTransform.position, dest, rotateTime));
    }
    IEnumerator MoveSmoothly(Transform transformToMove, Vector3 currentPos, Vector3 destPos, float secondsToComplete)
    {
        if (!isMovingSmoothly)
        {
            isMovingSmoothly = true;
            Vector3 movementVector = destPos - currentPos;
            float t = 0;
            do
            {
                t += Time.deltaTime;
                float moveAmount = Time.deltaTime / secondsToComplete;
                Vector3 moveVector = moveAmount * (movementVector);
                if (t >= secondsToComplete)
                {
                    transformToMove.position = destPos;
                    break;
                }
                transformToMove.transform.position += moveVector;

                yield return null;
            } while (t < secondsToComplete);

            isMovingSmoothly = false;
        }
        
    }
    //This coroutine rotates an object, around a point and axis at some angle, during the given time
    IEnumerator RotateSmoothly(Transform transformToRotate, Vector3 point, Vector3 axis, float angle, float secondsToComplete)
    {
        if(!isRotatingSmoothly)
        {
            isRotatingSmoothly = true;
            float angleRotated = 0;
            float t = 0;
            do
            {
                t += Time.deltaTime;
                float amountToRotate = Time.deltaTime * angle / secondsToComplete;
                if (t >= secondsToComplete)
                {
                    amountToRotate = angle - angleRotated;
                }
                transformToRotate.RotateAround(point, axis, amountToRotate);
                angleRotated += amountToRotate;

                yield return null;
            } while (t < secondsToComplete);

            isRotatingSmoothly = false;
        }
    }

    public void OnClick_Left()
    {
        playerNode = GameController.Game.LevelController.PlayerNode;
        StartCoroutine(RotateSmoothly(cameraObject.transform, playerNode.GetPosition(), Vector3.down, -90, rotateTime));
    }
    public void OnClick_Right()
    {
        playerNode = GameController.Game.LevelController.PlayerNode;
        StartCoroutine(RotateSmoothly(cameraObject.transform, playerNode.GetPosition(), Vector3.down, 90, rotateTime));
    }


}

public class Rotation
{

}
