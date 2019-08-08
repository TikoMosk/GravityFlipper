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

    private event Action OnPlayerMoveEvent;
    protected int turnCount;

    #region Public Methods

    public void RegisterOnEvent(Action action)
    {
        OnPlayerMoveEvent += action;
    }

    public void RemoveFromEvent(Action action)
    {
        OnPlayerMoveEvent -= action;
    }

    public void NextTurn()
    {
        //Invokes, when event is not empty;
        OnPlayerMoveEvent?.Invoke();
        turnCount++;
    }

    #endregion
}
