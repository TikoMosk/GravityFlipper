using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeNodeObject : MonoBehaviour, INodeObject
{

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

}
