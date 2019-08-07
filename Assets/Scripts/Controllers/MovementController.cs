using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject cameraObject;

    Node previousClickedNode;
    Node.Direction previousDirection;
    Node playerNode;
    private float t;
    private Vector3 start;
    private Vector3 v;

    public void MoveTo(Transform target, Vector3 destination, float duration) {
        
    }
    public IEnumerator MoveToCoroutine(Transform targ, Vector3 pos, float dur) {
        t = 0f;
        start = targ.position;
        v = pos - start;
        while (t < dur) {
            t += Time.deltaTime;
            targ.position = start + v * t / dur;
            yield return null;
        }

        targ.position = pos;
    }
    
        
}
