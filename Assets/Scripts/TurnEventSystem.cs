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
<<<<<<< HEAD:Assets/Scripts/TurnEventSystem.cs
=======
    #region Events

    private event Action onPlayerMove;
>>>>>>> parent of b4c594b... //test// laser, enemy
=======
    public GameObject[] lasers;
    public GameObject[] GetLasers()
    {
        return lasers;
    }
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System:Assets/Scripts/TurnSystem/TurnEventSystem.cs

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
