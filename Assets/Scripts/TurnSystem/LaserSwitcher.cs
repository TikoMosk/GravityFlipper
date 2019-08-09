using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitcher : MonoBehaviour
{
    public enum Active
    {
        Static,
        Active,
        Mid,
        Deactive,
    }
    public Active StartState;

    private bool _static;
    private bool _active;
    private bool _flag;

    private void Start()
    {
        if (StartState == Active.Static)
        {
            gameObject.SetActive(true);
            return;
        }

        switch (StartState)
        {
            case Active.Active:
                _active = true;
                _flag = true;
                break;
            case Active.Mid:
                _active = true;
                break;
        }
        Check();
        EventController.currentInstance.Register(Check);
    }

    private void Check()
    {
        if (_active)
        {
            if (_flag)
            {
                gameObject.SetActive(true);
                _active = !_active;
                _flag = !_flag;
            }
            else
            {
                gameObject.SetActive(false);
                _flag = !_flag;
            }
        }
        else
        {
            if (_flag)
                _active = !_active;
            else
                _flag = !_flag;

            gameObject.SetActive(false);
        }
    }
}
