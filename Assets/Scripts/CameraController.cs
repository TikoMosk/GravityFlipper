using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCam;
    public float dragAmountToChangeCamera;
    public float cameraSpeed;
    Vector2 touchPos;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 dragPos = touch.position;
                if(Vector2.Distance(touchPos,dragPos) > dragAmountToChangeCamera)
                {
                    ChangeCamera(touchPos - dragPos);
                }
            }
        }
    }
    private void ChangeCamera(Vector2 dragVector)
    {
        if(dragVector.x < 0)
        {
            mainCam.transform.RotateAround(Vector3.zero, Vector3.up,cameraSpeed * Time.deltaTime);
        }
        else if (dragVector.x > 0)
        {
            mainCam.transform.RotateAround(Vector3.zero, Vector3.down, cameraSpeed * Time.deltaTime);
        }
    }
}
