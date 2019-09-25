using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    private int num = 0;

    public void Start()
    {
        
    }

    public void AmountOfButtons(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            num++;
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(panelToAttachButtonsTo.transform);
            //button.GetComponent<Button>().onClick.AddListener(OnClick);
            button.transform.GetChild(0).GetComponent<Text>().text = "" + num;

        }
    }
}

