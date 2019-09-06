using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettingsController : MonoBehaviour
{
    public InputField widthField;
    public InputField heightField;
    public InputField lengthField;
    public void SaveSettings() {
        int width = int.Parse(widthField.text);
        int height = int.Parse(heightField.text);
        int length = int.Parse(lengthField.text);
        GameController.Game.CurrentLevel.ResizeLevel(width, height, length);
    }
}
