using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventController : MonoBehaviour
{
    private bool isEnabled;
    Vector3 dest;

    private void Start()
    {
        TurnEventSystem.currentInstance.RegisterOnEvent(Foo);
    }

    public void Foo()
    {
        dest = transform.position;
        dest.z -= 1;

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
        TurnEventSystem.currentInstance.RemoveFromEvent(Foo);
    }
}
