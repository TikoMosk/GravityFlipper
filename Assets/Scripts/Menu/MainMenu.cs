using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    AudioSource audioSource;

    public Text orthographic;
    public Text perspective;
    public Text on;
    public Text off;

    private bool enabled = true;
    private bool changed = true;

    public GameObject aboutMenuUI;
    public GameObject mainMenuUI;
    public GameObject playMenuUI;
    public GameObject settingsMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Music()
    {
        if (enabled == true)
        {
            on.gameObject.SetActive(false);
            off.gameObject.SetActive(true);

            PlayerPrefs.SetFloat("Volume", 0.0f);
            audioSource.volume = PlayerPrefs.GetFloat("Volume");
            enabled = false;
        }
        else
        {
            on.gameObject.SetActive(true);
            off.gameObject.SetActive(false);

            PlayerPrefs.SetFloat("Volume", 1f);
            audioSource.volume = PlayerPrefs.GetFloat("Volume");
            enabled = true;
        }

    }

    public void Camera()
    {
        if (changed == true)
        {
            perspective.gameObject.SetActive(false);
            orthographic.gameObject.SetActive(true);

            changed = false;
        }
        else
        {
            perspective.gameObject.SetActive(true);
            orthographic.gameObject.SetActive(false);

            changed = true;
        }
    }

    public void Play()
    {
        mainMenuUI.SetActive(false);
        playMenuUI.SetActive(true);
    }

    public void Settings()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void About()
    {
        mainMenuUI.SetActive(false);
        aboutMenuUI.SetActive(true);
    }

    public void BackToMainMenu()
    {
        if (mainMenuUI)
        {
            mainMenuUI.SetActive(true);
            playMenuUI.SetActive(false);
        }

        if (settingsMenuUI)
        {
            mainMenuUI.SetActive(true);
            settingsMenuUI.SetActive(false);
        }

        if (aboutMenuUI)
        {
            mainMenuUI.SetActive(true);
            aboutMenuUI.SetActive(false);
        }

    }

    public void OpenURL()
    {
        Application.OpenURL("http://frismos.com/");
    }
}
