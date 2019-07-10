using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCam;
    public float rotateTime;
    private IEnumerator rotateCoroutine;
    private bool isRotating;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotating)
        {
            rotateCoroutine = RotateCamera(new Vector2(-1, 0));
            StartCoroutine(rotateCoroutine);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotating)
        {
            rotateCoroutine = RotateCamera(new Vector2(1, 0));
            StartCoroutine(rotateCoroutine);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isRotating)
        {
            rotateCoroutine = RotateCamera(new Vector2(0, 1));
            StartCoroutine(rotateCoroutine);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isRotating)
        {
            rotateCoroutine = RotateCamera(new Vector2(0, -1));
            StartCoroutine(rotateCoroutine);
        }
    }
    IEnumerator RotateCamera(Vector2 cameraChange)
    {
        isRotating = true;
        for (float t = 0; t  <= rotateTime; t+= Time.deltaTime)
        {
            if (cameraChange.x == 0)
            {
                mainCam.transform.RotateAround(Vector3.zero, -Vector3.forward * cameraChange.y, Time.deltaTime * 90 / rotateTime);
            }
            else if (cameraChange.y == 0)
            {
                mainCam.transform.RotateAround(Vector3.zero, Vector3.down * cameraChange.x, Time.deltaTime * 90 / rotateTime);
            }
            
            yield return null;
        }
        isRotating = false;
        
    }

    public void OnClick_Left()
    {
        if (!isRotating)
        {
            rotateCoroutine = RotateCamera(new Vector2(-1, 0));
            StartCoroutine(rotateCoroutine);
        }
    }
    public void OnClick_Right()
    {
        if (!isRotating)
        {
            rotateCoroutine = RotateCamera(new Vector2(1, 0));
            StartCoroutine(rotateCoroutine);
        }
    }
    public void OnClick_Up()
    {
        if (!isRotating)
        {
            rotateCoroutine = RotateCamera(new Vector2(0, 1));
            StartCoroutine(rotateCoroutine);
        }
    }
    public void OnClick_Down()
    {
        if (!isRotating)
        {
            rotateCoroutine = RotateCamera(new Vector2(0, -1));
            StartCoroutine(rotateCoroutine);
        }
    }
}
