using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventController : MonoBehaviour
{
    private bool isEnabled;
    private byte flag = 1;
    private Node CurrentNode { get; set; }

    Vector3 dest;

    private void Start()
    {
        TurnEventSystem.currentInstance.RegisterOnEvent(Check);
    }

    public void Check()
    {
        dest = transform.position;
        dest.z -= flag;

        //todo
        if (true)
        {

        }

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
        TurnEventSystem.currentInstance.RemoveFromEvent(Check);
    }
}
