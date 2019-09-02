using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour {
    private ToggleButton connectedToggle;
    public NodePicker nodePicker;
    public Toggle firstToggle;
    public ToggleButton firstToggleButton;

    private void OnEnable() {
        
    }
    public void Connect(ToggleButton button) {
        connectedToggle = button;
        
    }
    public void Disable () {
        if (connectedToggle != null) {
            connectedToggle.SetOn(false);
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
