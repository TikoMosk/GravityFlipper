using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu currentInstance;
    private void Awake() => currentInstance = this;

    //public AudioMixer audioMixer;
    public Sprite MusicOff;
    public Sprite MusicOn;
    public Button musicButton;

    public Sprite SoundOff;
    public Sprite SoundOn;
    public Button soundButton;
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;

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
        GameController.Game.SmoothGraphics.AnimationCount = 0;
        GameController.Game.gameObject.SetActive(true);
        GameController.Game.LevelController.LoadLevelFromProject("level1.json");
        GameController.Game.CameraController.ResetCamera();
        if(gameOverMenuUI != null)
        {
            gameOverMenuUI.SetActive(false);
        }
        if (pauseMenuUI != null) {
            pauseMenuUI.SetActive(false);
        }
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        GameController.Game.gameObject.SetActive(false);
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void MainMenu()
    {
        GameController.Game.gameObject.SetActive(true);
        Time.timeScale = 1f;
        GameController.Game.ChangeScene("Main Menu");
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
