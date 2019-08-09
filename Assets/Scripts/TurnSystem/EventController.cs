using System;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    #region Singleton

    public static EventController currentInstance;
    private void Awake() { currentInstance = this; }

    #endregion

    public void Register(Action action)
    {
        TurnEventSystem.currentInstance.RegisterOnEvent(action);
    }

    public void Remove(Action action)
    {
        TurnEventSystem.currentInstance.RemoveFromEvent(action);
    }
}
