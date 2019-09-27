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
    public int turnCount;

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
        OnPlayerMoveEvent?.Invoke();
        turnCount++;
    }
    
    #endregion
}
