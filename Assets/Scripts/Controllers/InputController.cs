using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }
    private void Click()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.GetComponent<NodeGraphic>() != null)
            {
                hit.collider.gameObject.GetComponent<NodeGraphic>().GetClicked(GetDirectionByNormal(hit.normal));
            }
        }
    }
    private Node.Direction GetDirectionByNormal(Vector3 normal)
    {
        Vector3 localNormal = GameController.Game.levelController.transform.TransformPoint(normal);
        Node.Direction dir = Node.Direction.UP;
        if (normal.Equals(new Vector3(1, 0, 0))) dir = Node.Direction.RIGHT;
        else if (normal.Equals(new Vector3(-1, 0, 0))) dir = Node.Direction.LEFT;
        else if (normal.Equals(new Vector3(0, 1, 0))) dir = Node.Direction.UP;
        else if (normal.Equals(new Vector3(0, -1, 0))) dir = Node.Direction.DOWN;
        else if (normal.Equals(new Vector3(0, 0, 1))) dir = Node.Direction.FORWARD;
        else if (normal.Equals(new Vector3(0, 0, -1))) dir = Node.Direction.BACK;

        return dir;

    }
}
