using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Click(0);
        }
        if (Input.GetMouseButtonDown(1))
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
                hit.collider.gameObject.GetComponent<NodeGraphic>().GetClicked(GetDirectionByNormal(hit.normal), button);
            }
        }
    }
    private Node.Direction GetDirectionByNormal(Vector3 normal)
    {
        Vector3 localNormal = GameController.Game.LevelController.transform.InverseTransformPoint(normal);

        Node.Direction dir = Node.Direction.UP;
        if (Vector3.SqrMagnitude(localNormal - new Vector3(1, 0, 0)) < 0.1f) dir = Node.Direction.RIGHT;
        else if (Vector3.SqrMagnitude(localNormal -new Vector3(-1, 0, 0)) < 0.1f) dir = Node.Direction.LEFT;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(0, 1, 0)) < 0.1f) dir = Node.Direction.UP;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(0, -1, 0)) < 0.1f) dir = Node.Direction.DOWN;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(0, 0, 1)) < 0.1f) dir = Node.Direction.FORWARD;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(0, 0, -1)) < 0.1f) dir = Node.Direction.BACK;
        return dir;

    }
    
}
