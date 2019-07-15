using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeNodeObject : MonoBehaviour, ISolidNodeObject
{

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

}
