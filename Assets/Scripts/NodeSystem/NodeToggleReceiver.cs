using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeToggleReceiver : MonoBehaviour
{
    private Action onToggle;
    public void RegisterToToggleReceiver(Action onToggle) {
        this.onToggle += onToggle;
    }
    public void UnRegisterToToggleReceiver(Action onToggle) {
        this.onToggle -= onToggle;
    }
    public void Trigger() {
        if(onToggle != null) {
            onToggle.Invoke();
        }
    }
}
