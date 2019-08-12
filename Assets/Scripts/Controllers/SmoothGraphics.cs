using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmoothGraphics : MonoBehaviour
{
    int animationCount = 0;

    public int AnimationCount { get => animationCount; set => animationCount = value; }

    public IEnumerator MoveSmoothly(Transform obj, Vector3 target, float overTime) {
        AnimationCount++;
        float startTime = Time.time;
        while (Time.time < startTime + overTime) {
            obj.transform.position = Vector3.Lerp(obj.transform.position, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        obj.transform.position = target;
        AnimationCount--;
    }
    public IEnumerator RotateSmoothly(Transform obj, Quaternion targetRotation, float overTime) {
        AnimationCount++;
        float startTime = Time.time;
        while (Time.time < startTime + overTime) {
            obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, targetRotation, (Time.time - startTime) / overTime);
            yield return null;
        }
        obj.transform.rotation = targetRotation;
        AnimationCount--;
    }
}

