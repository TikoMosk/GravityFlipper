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

    private event Action onPlayerMoveEvent;
    public GameObject[] lasers;
    public GameObject[] GetLasers()
    {
        return lasers;
    }

    #region Public Methods

    public void RegisterOnEvent(Action action)
    {
        onPlayerMoveEvent += action;
    }

    public void RemoveFromEvent(Action action)
    {
        onPlayerMoveEvent -= action;
    }

    public void NextTurn()
    {
        onPlayerMoveEvent?.Invoke(); //Invoke if event is not empty;
    }

    #endregion
}
