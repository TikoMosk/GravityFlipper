using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeNodeObject : MonoBehaviour, INodeHolder
{

    public List<CubeNodeObject> adjacencyList = new List<CubeNodeObject>();

    public bool walkable = false;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;


    private void OnMouseExit()
    {
        //transform.GetComponent<Renderer>().material.color = Color.white;
        
    }

    private void OnMouseOver()
    {
        //transform.GetComponent<Renderer>().material.color = Color.blue;
        //Instantiate(new Light(), gameObject.transform.position, Quaternion.identity);
       // Destroy(gameObject);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }

        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
