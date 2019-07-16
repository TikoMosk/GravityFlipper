using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNodeObject : MonoBehaviour, INodeObject
{
    public Node FindNode()
    {
        return Level.Controller.nodesArray[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z];
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
