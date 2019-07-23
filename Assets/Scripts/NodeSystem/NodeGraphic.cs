using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeGraphic : MonoBehaviour
{
    public event Action onClick;

    private void OnMouseDown()
    {
        if (onClick != null)
            onClick();
    }
}
