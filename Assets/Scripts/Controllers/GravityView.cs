using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GravityView : MonoBehaviour
{
    public GameObject worldParent;
    public Camera mainCam;
    public GameObject cameraObject;
    public float rotateTime;

    private Vector3 startPos;
    private bool isRotatingSmoothly;
    private bool isMovingSmoothly;
    private bool isDragging;
    private Vector3 playerPos;

    private void Awake()
    {
        startPos = mainCam.transform.position;
    }
    private void Update()
    {
        if(GameController.Game.currentLevel != null)
        {
            playerPos = GameController.Game.currentLevel.GetPlayerNode().GetPosition();
            cameraObject.transform.position = playerPos;
        }
    }

    public void RotateWorld(Vector3 up,Vector3 forward)
    {

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
                Vector3 moveVector = moveAmount * (destPos - currentPos);
                if (t >= secondsToComplete)
                {
                    transformToMove.position = destPos;
                    break;
                }
                transformToMove.Translate(moveVector);

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

    // Checks which direction, the world should rotate, given the drag position
    // THIS IS TEMPORARY AND IS GOING TO BE CHANGED
    private Vector3 CheckDragDirection(Vector2 dragVector)
    {
        if(dragVector.y <= 0)
        {
            if(dragVector.x >= 0)
            {
                return new Vector3(0,0,1);
            }
            if (dragVector.x < 0)
            {
                return new Vector3(1,0,0);
            }
        }
        else
        {
            if (dragVector.x >= 0)
            {
                return new Vector3(-1, 0,0 );
            }
            if (dragVector.x < 0)
            {
                return new Vector3(0, 0, -1);
            }
        }
        return Vector2.zero;
    }


    public void OnClick_Left()
    {
        Debug.Log(playerPos);
        StartCoroutine(RotateSmoothly(cameraObject.transform, playerPos, Vector3.down, -90, rotateTime));
    }
    public void OnClick_Right()
    {
        Debug.Log(playerPos);
        StartCoroutine(RotateSmoothly(cameraObject.transform, playerPos, Vector3.down, 90, rotateTime));
    }


}
