using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsData : MonoBehaviour
{
    private string path;
    private string filename;
    

    private void Start()
    {
        path = Application.persistentDataPath + "/" + filename;
    }
}
