using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private static GameController _game;
    public static GameController Game { get { return _game; } }

    private Level currentLevel = null;
    public Level CurrentLevel { get { return currentLevel; } }

    private LevelDesignController levelDesignController;
    private MovementController movementController;
    private SmoothGraphics smoothGraphics;
    private LevelController levelController;
    private CameraController cameraController;

    public LevelController LevelController { get { return levelController; } }
    public LevelDesignController LevelDesignController { get { return levelDesignController; } }
    public MovementController MovementController { get { return movementController; } }
    public SmoothGraphics SmoothGraphics { get { return smoothGraphics; } }
    public CameraController CameraController { get { return cameraController; } }

    public GameState CurrentGameState { get => gameState; set => gameState = value; }

    private Action onNextTurn;

    private GameState gameState;

    private void Awake() {
        SetUpSingleton();
        SetUpControllers();

    }
    private void Start() {
        SetGameModeBasedOnScene();
    }
    public void TestLevel() {
        Time.timeScale = 1f;
        levelController.BuildTestLevel();
        Debug.Log(cameraController);
        cameraController.ResetCamera();
    }
    private void SetUpSingleton() {
        if (_game != null && _game != this) {
            Destroy(this);
        }
        else {
            _game = this;
        }
    }
    public void SetGameModeBasedOnScene() {
        if (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "OfficialLevels") {
            CurrentGameState = new PlayMode();

        }
        else if (SceneManager.GetActiveScene().name == "LevelEditor") {
            CurrentGameState = new LevelEditorMode();
        }
    }
    public void ChangeGameState(String stateName) {
        if (stateName == "PlayMode") {
            gameState = new PlayMode();
        }
        if (stateName == "LevelEditorMode") {
            gameState = new LevelEditorMode();
        }

    }
    public void ChangeScene(string s) {
        try {
            SceneManager.GetSceneByName(s);
        }
        catch {
            Debug.LogError("Scene by the name " + s + " does not exist");
            return;
        }

        SceneManager.LoadScene(s);
    }
    public void ClickNode(Node n, Node.Direction dir) {
        CurrentGameState.OnNodeClick(n, dir);
    }

    private void SetUpControllers() {
        levelController = FindObjectOfType<LevelController>();
        if (levelController != null) {
            levelController.RegisterToLevelCreated(CurrentLevelActive);
        }
        movementController = FindObjectOfType<MovementController>();
        levelDesignController = FindObjectOfType<LevelDesignController>();
        smoothGraphics = FindObjectOfType<SmoothGraphics>();
        cameraController = FindObjectOfType<CameraController>();
    }
    private void CurrentLevelActive() {
        currentLevel = levelController.Level;
    }
    public void NextTurn() {

    }
    public void RegisterForNextTurn(Action onNextTurn) {
        this.onNextTurn += onNextTurn;
    }




}
