using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeNodeObject : MonoBehaviour, INodeObject
{
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public Node FindNode()
    {
        return Level.Controller.nodesArray[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z];
    }

    /// <summary>
    /// Moves object to x,y,z node
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    private void _Move(Node node)
    {
        //TODO
        Node _playerNode = Level.Controller.playerNode;
        _playerNode.ChangeObjectByNode(node);
    }

    private bool _IsReachable()
    {
        Node _playerNode = Level.Controller.playerNode;
        Vector3 _playerPos = _playerNode.GetNodePosition();
        Debug.Log(_playerPos.x);
        Debug.Log(gameObject.transform.position.x);

        if ((int)_playerPos.y == (int)gameObject.transform.position.y + 1)
        {
            Debug.Log("same y");
            if (Mathf.Abs((int)_playerPos.x - (int)gameObject.transform.position.x) == 1)
            {
                Debug.Log("same x");
                return true;
            }

            if (Mathf.Abs((int)_playerPos.z - (int)gameObject.transform.position.z) == 1)
            {
                Debug.Log("same z");
                return true;
            }
        }

        // TODO
        return false;
    }

    private void OnMouseDown()
    {
        Debug.Log("mouse down");
        if (_IsReachable())
        {
            Debug.Log("reachable");
            _Move(this.FindNode());
        }
    }
}
