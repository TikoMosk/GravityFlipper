using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    private bool pressed;
    private GameObject[] lasers;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            pressed = true;
            lasers = TurnEventSystem.currentInstance.GetLasers();

            foreach (GameObject laser in lasers)
            {
                laser.SetActive(false);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (pressed)
        {
            pressed = false;
            lasers = TurnEventSystem.currentInstance.GetLasers();

            foreach (GameObject laser in lasers)
            {
                laser.SetActive(true);
            }
        }
    }
}
