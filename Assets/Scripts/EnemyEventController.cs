using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventController : MonoBehaviour
{
<<<<<<< HEAD
    private bool isEnabled;
    private byte flag = 1;
    Vector3 dest;

    private void Start()
    {
<<<<<<< HEAD:Assets/Scripts/EnemyEventController.cs
        TurnEventSystem.currentInstance.RegisterOnEvent(Check);
=======
	private bool isEnable;
    private void Start()
    {
		TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
>>>>>>> parent of b4c594b... //test// laser, enemy
=======
        TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System:Assets/Scripts/TurnSystem/EnemyEventController.cs
    }

    public void Foo()
    {
<<<<<<< HEAD
        dest = transform.position;

        dest.z -= flag;

        isEnabled = !isEnabled;
    }

    private void Update()
    {
        if (isEnabled)
        {
            Move();
        }

        if (transform.position == dest)
        {
            isEnabled = false;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * 5);
    }

    private void OnDestroy()
    {
<<<<<<< HEAD:Assets/Scripts/EnemyEventController.cs
        TurnEventSystem.currentInstance.RemoveFromEvent(Check);
=======
		if (!isEnable)
			transform.localScale = transform.localScale * 2;
		else
			transform.localScale = transform.localScale / 2;

        isEnable = !isEnable;
>>>>>>> parent of b4c594b... //test// laser, enemy
=======
        TurnEventSystem.currentInstance.RemoveFromEvent(Foo);
>>>>>>> parent of e580fef... Merge branch 'nodeSystem' into Turn-System:Assets/Scripts/TurnSystem/EnemyEventController.cs
    }
}
