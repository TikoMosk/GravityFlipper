using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCam;
    public float rotateTime;
    private IEnumerator rotateCoroutine;
    private bool isRotating;

    IEnumerator RotateCamera(float x)
    {
        isRotating = true;
        float angleRotated = 0;
        float t = 0;
        do
        {
            t += Time.deltaTime;
            float amountToRotate = Time.deltaTime * 90 / rotateTime;
            if (t >= rotateTime)
            {
                amountToRotate = 90 - angleRotated;
            }    
            mainCam.transform.RotateAround(Vector3.zero, Vector3.down * x, amountToRotate);
            angleRotated += amountToRotate;

            yield return null;
        } while (t < rotateTime);

        isRotating = false;
        
    }

    public void OnClick_Left()
    {
        if (!isRotating)
        {
            rotateCoroutine = RotateCamera(-1);
            StartCoroutine(rotateCoroutine);
        }
    }
    public void OnClick_Right()
    {
        if (!isRotating)
        {
            rotateCoroutine = RotateCamera(1);
            StartCoroutine(rotateCoroutine);
        }
    }
   
}
