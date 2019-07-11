using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public GameObject houseObject;
    public float worldRotateTime;
    private Vector2 touchStart;
    private Vector2 touchPos;
    private Vector2 touchDragVector;
    
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            if(touch.phase == TouchPhase.Moved)
            {
                touchPos = touch.position;
                touchDragVector = touchPos - touchStart;
                
            }
            if(touch.phase == TouchPhase.Ended)
            {
                FlipWorld(CheckDragDirection(touchDragVector));
            }
           
        }
    }
    private void FlipWorld(Vector3 direction)
    {
        if(direction != Vector3.zero)
        {

            StartCoroutine(RotateWorld(direction));
        }
    }
    IEnumerator RotateWorld(Vector3 direction)
    {
        float angleRotated = 0;
        float t = 0;
        do
        {
            
            t += Time.deltaTime;
            float amountToRotate = Time.deltaTime * 90 / worldRotateTime;
            if (t >= worldRotateTime)
            {
                amountToRotate = 90 - angleRotated;
            }
            houseObject.transform.RotateAround(houseObject.transform.position,direction.normalized, amountToRotate);
            angleRotated += amountToRotate;

            yield return null;
        } while (t < worldRotateTime);
    }
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

}
