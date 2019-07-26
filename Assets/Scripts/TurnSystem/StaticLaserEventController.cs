using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLaserEventController : MonoBehaviour
{
    public bool Static;
    public bool isActive;
    public bool flag;

    private void Start()
    {
        if (!Static)
        {
            gameObject.SetActive(isActive && flag);
            isActive = !isActive;
            flag = !flag;
            TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
        }
        gameObject.SetActive(true);
    }

    private void Foo()
    {
        if (isActive)
        {
            if (flag)
            {
                gameObject.SetActive(true);
                isActive = !isActive;
                flag = !flag;
            }
            else
            {
                gameObject.SetActive(false);
                flag = !flag;
            }
        }
        else
        {
            if (flag)
                isActive = !isActive;
            else
                flag = !flag;

            gameObject.SetActive(false);
        }

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
