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

<<<<<<< HEAD
    private event Action onPlayerMoveEvent;
=======
    #region Events

    private event Action onPlayerMove;
>>>>>>> parent of b4c594b... //test// laser, enemy

    #region Public Methods

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
