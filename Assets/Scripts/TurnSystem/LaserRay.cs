using System.Collections;
using System.Collections.Generic;
using VolumetricLines;
using UnityEngine;

public class LaserRay : MonoBehaviour
{
    public float startLength;
    private float Length;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Node startNode;

    private void Start()
    {
        EventController.currentInstance.Register(Check);

        Length = startLength;
        startNode = GameController.Game.CurrentLevel.GetNode(startPoint);
        startPoint = startNode.GetPosition();
        endPoint = startPoint + new Vector3(0, 0, -startLength + 0.5f);
        GetComponent<VolumetricLineBehavior>().StartPos = this.startPoint;
        GetComponent<VolumetricLineBehavior>().EndPos = this.endPoint;
    }

    private void Check()
    {
        if (GameController.Game.CurrentLevel.Player.Graphic.Node.GetPosition() == new Vector3(4, 1, 2))
        {
            GetComponent<VolumetricLineBehavior>().LineColor = Color.green;
        }
        else
        {
            GetComponent<VolumetricLineBehavior>().LineColor = Color.red;
        }

        if (GameController.Game.CurrentLevel.Player.Graphic.Node.GetPosition() == new Vector3(3, 1, 2))
        {
            StartCoroutine("LaserOff");
            GetComponent<VolumetricLineBehavior>().EndPos = new Vector3(0, 0, -startLength + 0.5f);
        }
        else
        {
            StartCoroutine("LaserOn");
            GetComponent<VolumetricLineBehavior>().EndPos = new Vector3(startLength - 0.5f, 0, 0);
        }
    }

    IEnumerator LaserOff()
    {
        while (startLength > 0)
        {
            Debug.Log(startLength);
            yield return null;
            startLength -= 0.1f;
        }
    }

    IEnumerator LaserOn()
    {
        while (startLength <= Length)
        {
            yield return null;
            startLength += 0.1f;
        }
    }
}
