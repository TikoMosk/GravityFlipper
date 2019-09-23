using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log(numberOfLevels());
    }

    public int numberOfLevels()
    {
        int number;
        int.TryParse(GetComponentInChildren<Text>().text, out number);
        return number;
    }
}
