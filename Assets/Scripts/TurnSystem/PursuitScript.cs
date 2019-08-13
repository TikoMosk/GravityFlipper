using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitScript : MonoBehaviour
{
    private Vector3[] directions;
    private void Start()
    {
        EventController.currentInstance.Register(Check);

        directions = new Vector3[6];
        directions[0] = new Vector3(1, 0, 0);
        directions[1] = new Vector3(0, 1, 0);
        directions[2] = new Vector3(0, 0, 1);
        directions[3] = new Vector3(-1, 0, 0);
        directions[4] = new Vector3(0, -1, 0);
        directions[5] = new Vector3(0, 0, -1);
    }

    public void StartPursuit()
    {
        Debug.Log("StartPursuit");
    }

    public void EndPursuit()
    {
        GetComponent<PatrolScript>().enabled = true;
    }

    private void Check()
    {
        Vector3 vector = new Vector3();
        foreach (var step in directions)
        {
            vector = transform.position + step;
            if (GameController.Game.CurrentLevel.GetNode((int)vector.x, (int)vector.y, (int)vector.z).NodeMember != null)
            {
                if (GameController.Game.CurrentLevel.GetNode((int)vector.x, (int)vector.y, (int)vector.z).NodeMember.Id == 1)
                {
                    Debug.Log(vector);
                    StartPursuit();
                }
            }

        }
    }
}
