using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    Vector2 touchStart;
    Vector2 touchPos;
    Vector2 touchDragVector;
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
            }
            touchDragVector = touchPos - touchStart;
            Debug.Log(touchDragVector);
        }
    }

}
