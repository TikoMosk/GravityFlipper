using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Click(0);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Click(1);
        }
    }
    private void Click(int button)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            
            if(hit.collider.gameObject.GetComponent<NodeGraphic>() != null)
            {
                hit.collider.gameObject.GetComponent<NodeGraphic>().GetClicked(Dir.GetDirectionByVector(hit.normal), button);
            }
        }
    }
    
    
}
