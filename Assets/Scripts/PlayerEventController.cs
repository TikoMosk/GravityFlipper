using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventController : MonoBehaviour
{
	private bool isEnable;
    private void Start()
    {
		TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
        TurnEventSystem.currentInstance.RemoveFromEvent(Foo);
    }

    private void Foo()
    {
		if (!isEnable)
			transform.localScale = transform.localScale * 2;
		else
			transform.localScale = transform.localScale / 2;

		isEnable = !isEnable;
	}

    private void OnMouseDown()
    {
		TurnEventSystem.currentInstance.NextTurn();
	}

    void Update()
    {

    }
}
