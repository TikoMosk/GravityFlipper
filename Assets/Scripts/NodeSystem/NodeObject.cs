using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeObject
{
    private string type;
    private Node node;
    public GameObject gameObject;
    
    /// <summary>
    /// Sets the GameObject for the nodeObject
    /// </summary>
    /// <param name="_gameObject"></param>
    public void SetGameObject(GameObject _gameObject)
    {
        gameObject = _gameObject;
    }
    /// <summary>
    /// Returns the id (or type) of the nodeObject
    /// </summary>
    /// <returns></returns>
    public string GetNodeObjectType()
    {
        return type;
    }
    /// <summary>
    /// Sets the id (or type) of the nodeObject
    /// </summary>
    /// <param name="_type"></param>
    public void SetNodeObjectType(string _type)
    {
        type = _type;
    }

}
