using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Color normalColor;
    public Color selectedColor;
    public NodePicker nodePicker;
    private void Awake() {
        OnChangeValue();
    }
    public void OnChangeValue() {
        Toggle t = GetComponent<Toggle>();
        bool tOn = t.isOn;
        

        if (tOn) {
            GetComponent<Image>().color = selectedColor;
        }
        else {
            GetComponent<Image>().color = normalColor;
        }
    }
    public void ToggleWindow(Window window) {
        Toggle t = GetComponent<Toggle>();
        bool tOn = t.isOn;
        window.Connect(this);
        if (tOn) {
            window.gameObject.SetActive(true);
        }
        else {
            window.gameObject.SetActive(false);
        }
    }
    public void SetOn(bool value) {

        Toggle t = GetComponent<Toggle>();
        t.isOn = value;
    }
    public void OpenCategoryMenu(int cat) {
        
        Toggle t = GetComponent<Toggle>();
        bool tOn = t.isOn;
        if (tOn) {
            nodePicker.PopulatePanelByCategory(cat);
        }
        
    }
}
