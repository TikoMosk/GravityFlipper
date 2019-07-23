using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventController : MonoBehaviour
{
	private bool isEnable;
    private void Start()
    {
		TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
    }

    public void Foo()
    {
		if (!isEnable)
			transform.localScale = transform.localScale * 2;
		else
			transform.localScale = transform.localScale / 2;

        isEnable = !isEnable;
    }
}
