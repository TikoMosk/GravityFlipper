using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNodeObject : MonoBehaviour, INodeHolder
{
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
