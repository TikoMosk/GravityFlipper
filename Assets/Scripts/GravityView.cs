using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityView : MonoBehaviour
{
    public GameObject worldParent;
    public Vector3 playerPosition;
    public Camera mainCam;
    public float rotateTime;

    private Vector2 touchStart;
    private Vector2 touchPos;
    private Vector2 touchDragVector;
    private bool isRotatingSmoothly;
    private bool isDragging;

    private void Update()
    {
        //Calculates the mousePress/touch drag vector
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            touchPos = Input.mousePosition;
            touchDragVector = touchPos - touchStart;
        }

        // If drag ends, rotates the level.
        // This should work differently, when we add player movement. Instead of just rotating the world, it should move the player 
        // and rotate the world accordingly 
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(RotateSmoothly(worldParent.transform, playerPosition, CheckDragDirection(touchDragVector), 90, rotateTime));
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
        StartCoroutine(RotateSmoothly(mainCam.transform, playerPosition, Vector3.down, -90, rotateTime));
    }
    public void OnClick_Right()
    {

        StartCoroutine(RotateSmoothly(mainCam.transform, playerPosition, Vector3.down, 90, rotateTime));
    }


}
