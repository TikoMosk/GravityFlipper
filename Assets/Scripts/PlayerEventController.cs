using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventController : MonoBehaviour
{
    private void OnMouseDown()
    {
        TurnEventSystem.currentInstance.NextTurn();
    }

    void Update()
    {

    }
}
