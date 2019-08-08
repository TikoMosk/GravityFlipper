using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public enum Rotation 
    {
        HorizontalX,
        HorizontalZ,
        Vertical,
    }
    public Rotation rotation;

    private void Start()
    {
        switch (rotation)
        {
            case Rotation.HorizontalX:
                transform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 90));
                break;
            case Rotation.HorizontalZ:
                transform.rotation = Quaternion.AngleAxis(90, new Vector3(90, 0, 0));
                break;
        }

        Debug.Log(rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    } 
}
