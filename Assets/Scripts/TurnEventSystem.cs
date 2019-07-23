using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEventSystem : MonoBehaviour
{
    #region Singleton

    public static TurnEventSystem currentInstance;
    private void Awake() { currentInstance = this; }

    #endregion

    #region Events

    private event Action onPlayerMove;

    public void RegisterOnEvent(Action action)
    {
        onPlayerMove += action;
    }

    public void RemoveFromEvent(Action action)
    {
        onPlayerMove += action;
    }

    public void NextTurn()
    {
        onPlayerMove?.Invoke(); //Invoke if event is not empty;

    }

    #endregion
}
