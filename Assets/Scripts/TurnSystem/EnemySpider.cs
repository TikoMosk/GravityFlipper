using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{
    public bool Reverse;
    public enum Rotation
    {
        OnGround,
        OnWallX,
        OnWallZ
    }
    public Rotation rotation;

    private void Start()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        float rot = 0;
        Vector3 axis = new Vector3();

        switch (rotation)
        {
            case Rotation.OnGround:
                if (Reverse)
                    rot = 180;
                axis = Vector3.right;
                break;
            case Rotation.OnWallX:
                axis = Vector3.right;
                rot = -90;
                break;
            case Rotation.OnWallZ:
                axis = Vector3.forward;
                rot = 90;
                break;
        }

        if (Reverse)
            rot = -rot;

        transform.rotation = Quaternion.AngleAxis(rot, axis);
    }
}
