using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNodeObject : MonoBehaviour, INodeObject
{
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
