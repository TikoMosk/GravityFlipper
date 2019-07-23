using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLaserEventController : MonoBehaviour
{
    private bool isEnable;
    private void Start()
    {
        TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
    }

    private void Foo()
    {
        if (!isEnable)
            transform.localScale = transform.localScale * 2;
        else
            transform.localScale = transform.localScale / 2;

        isEnable = !isEnable;
    }
}
