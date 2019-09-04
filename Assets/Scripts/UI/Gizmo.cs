using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public enum GizmoType { Move, Rotate };
    public GizmoType gizmoType;
    public bool plus;
}
