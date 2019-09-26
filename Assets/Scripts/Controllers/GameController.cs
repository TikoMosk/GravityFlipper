using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _game;
    public static GameController Game { get { return _game; } }

    private Level currentLevel = null;
    public Level CurrentLevel { get { return currentLevel; } }

    private LevelDesignController levelDesignController;
    private SmoothGraphics smoothGraphics;
    private LevelController levelController;
    private CameraController cameraController;
    private AudioController audioController;
    private string levelPath;

    public LevelController LevelController { get { return levelController; } }
    public LevelDesignController LevelDesignController { get { return levelDesignController; } }
    public SmoothGraphics SmoothGraphics { get { return smoothGraphics; } }
    public CameraController CameraController { get { return cameraController; } }

    public IGameState CurrentGameState { get => gameState; set => gameState = value; }
    public string LevelPath { get => levelPath; set => levelPath = value; }
    public AudioController AudioController { get => audioController;  }

    private Action onNextTurn;
    private Action onGameStateChanged;

    private IGameState gameState;

    public GameObject winWindow;
    public GameObject loadingScreen;

    public Slider slider;

    
    private void Awake()
    {
        SetUpSingleton();
        SetUpControllers();

    }
    private void Start()
    {
        SetGameModeBasedOnScene();
        Time.timeScale = 1f;

    }
    public void TestLevel()
    {
        Time.timeScale = 1f;
        levelController.LoadLevelFromProject("testLevel.json");
        cameraController.ResetCamera();
    }
    private void LoadLevel() {
        levelController.LoadLevelFromProject(levelPath);
    }
    private void Update()
    {
        gameState.Update();
    }
    private void SetUpSingleton()
    {
        if (_game != null && _game != this)
        {
            Destroy(this);
        }
        else
        {
            _game = this;
        }
    }
    public void SetGameModeBasedOnScene()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "OfficialLevels")
        {
            CurrentGameState = new PlayMode();

        }
        else if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            CurrentGameState = new LevelEditorMode();
        }
        else
        {
            CurrentGameState = new MenuMode();
        }

    }
    public void ChangeGameState(String stateName)
    {
        Time.timeScale = 1f;

        if (stateName == "PlayMode")
        {
            gameState = new PlayMode();
        }
        if (stateName == "LevelEditorMode")
        {
            gameState = new LevelEditorMode();
        }
        if (stateName == "TestMode")
        {
            gameState = new TestMode();
        }
        if (onGameStateChanged != null)
        {
            onGameStateChanged.Invoke();
        }

    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeScene(string s)
    {
        try
        {
            SceneManager.GetSceneByName(s);
        }
        catch
        {
            Debug.LogError("Scene by the name " + s + " does not exist");
            return;
        }

        SceneManager.LoadScene(s);

    }

    public void ChangeSceneLoading(string s)
    {
        try
        {
            SceneManager.GetSceneByName(s);
        }
        catch
        {
            Debug.LogError("Scene by the name " + s + " does not exist");
            return;
        }

        StartCoroutine(LoadAsynchronously(s));

    }

    IEnumerator LoadAsynchronously(string s)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(s);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            //progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void ClickNode(Node n, Node.Direction dir)
    {
        CurrentGameState.OnNodeClick(n, dir);
    }
    public void Win()
    {
        StartCoroutine(WaitForAnimationEnd());
    }
    IEnumerator WaitForAnimationEnd()
    {
        while (smoothGraphics.AnimationCount > 0)
        {
            yield return null;
        }
        winWindow.SetActive(true);
        Time.timeScale = 0;
    }

    private void SetUpControllers()
    {
        cameraController = FindObjectOfType<CameraController>();
        levelController = FindObjectOfType<LevelController>();
        if (levelController != null)
        {
            levelController.RegisterToLevelCreated(CurrentLevelActive);
        }
        audioController = FindObjectOfType<AudioController>();
        levelDesignController = FindObjectOfType<LevelDesignController>();
        smoothGraphics = FindObjectOfType<SmoothGraphics>();
    }
    private void CurrentLevelActive()
    {
        currentLevel = levelController.Level;
    }
    public void NextTurn()
    {

    }
    public void RegisterForNextTurn(Action onNextTurn)
    {
        this.onNextTurn += onNextTurn;
    }
    public void RegisterForGameStateChanged(Action gameStateChanged)
    {
        this.onGameStateChanged += gameStateChanged;
    }
    public void UnRegisterForGameStateChanged(Action gameStateChanged)
    {
        this.onGameStateChanged -= gameStateChanged;
    }




}
