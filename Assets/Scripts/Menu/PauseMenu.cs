using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //public AudioMixer audioMixer;
    public Sprite MusicOff;
    public Sprite MusicOn;
    public Button musicButton;

    public Sprite SoundOff;
    public Sprite SoundOn;
    public Button soundButton;
    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {

    }

    public void PauseButton()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        GameController.Game.gameObject.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        GameController.Game.gameObject.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void SetMusicVolume()
    {
       if(musicButton.image.sprite == MusicOn)
        {
            musicButton.image.sprite = MusicOff;
            Debug.Log("Music is OFF");
        }
       else
        {
            musicButton.image.sprite = MusicOn;
            Debug.Log("Music is On");
        }
    }

    public void SetSoundVolume()
    {
        if (soundButton.image.sprite == SoundOn)
        {
            soundButton.image.sprite = SoundOff;
            Debug.Log("Music is OFF");
        }
        else
        {
            soundButton.image.sprite = SoundOn;
            Debug.Log("Music is On");
        }
    }
}
