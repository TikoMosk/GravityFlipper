using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dir
{
    public static Node.Direction GetDirectionByVector(Vector3 normal) {
        Vector3 localNormal = GameController.Game.LevelController.transform.InverseTransformPoint(normal);

        Node.Direction dir = Node.Direction.UP;
        if (Vector3.SqrMagnitude(localNormal - new Vector3(1, 0, 0)) < 0.1f) dir = Node.Direction.RIGHT;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(-1, 0, 0)) < 0.1f) dir = Node.Direction.LEFT;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(0, 1, 0)) < 0.1f) dir = Node.Direction.UP;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(0, -1, 0)) < 0.1f) dir = Node.Direction.DOWN;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(0, 0, 1)) < 0.1f) dir = Node.Direction.FORWARD;
        else if (Vector3.SqrMagnitude(localNormal - new Vector3(0, 0, -1)) < 0.1f) dir = Node.Direction.BACK;
        return dir;

    }
    public static Vector3 GetVectorByDirection(Node.Direction dir) {
        if (dir == Node.Direction.RIGHT) {
            return new Vector3(1, 0, 0);
        }
        else if (dir == Node.Direction.LEFT) {
            return new Vector3(-1, 0, 0);
        }
        else if (dir == Node.Direction.UP) {
            return new Vector3(0, 1, 0);
        }
        else if (dir == Node.Direction.DOWN) {
            return new Vector3(0, -1, 0);
        }
        else if (dir == Node.Direction.FORWARD) {
            return new Vector3(0, 0, 1);
        }
        else if (dir == Node.Direction.BACK) {
            return new Vector3(0, 0, -1);
        }
        else {
            Debug.Log("ERROR");
            return Vector3.zero;
        }
    }
}
