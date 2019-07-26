using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLaserEventController : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    private bool isEnable;
    private void Start()
    {
        TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
>>>>>>> parent of b4c594b... //test// laser, enemy
    }

    private void Foo()
    {
<<<<<<< HEAD
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
=======
        if (!isEnable)
            transform.localScale = transform.localScale * 2;
        else
            transform.localScale = transform.localScale / 2;
>>>>>>> parent of b4c594b... //test// laser, enemy

        isEnable = !isEnable;
    }
}
