using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLaserEventController : MonoBehaviour
{
    public bool isActive;
    private bool isEnabled;
    private void Start()
    {
        gameObject.SetActive(isActive);
        isEnabled = isActive;
        TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
    }

    private void Foo()
    {
        isEnabled = !isEnabled;
        gameObject.SetActive(isEnabled);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        TurnEventSystem.currentInstance.RemoveFromEvent(Foo);
    }
}
