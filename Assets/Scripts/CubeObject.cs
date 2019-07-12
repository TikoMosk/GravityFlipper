using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObject : MonoBehaviour, INodeHolder
{
    private void OnMouseExit()
    {
        //transform.GetComponent<Renderer>().material.color = Color.white;
        
    }

    private void OnMouseOver()
    {
        //transform.GetComponent<Renderer>().material.color = Color.blue;
        Instantiate(new Light(), gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
