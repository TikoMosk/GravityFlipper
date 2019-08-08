using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _game;
    public static GameController Game{ get { return _game; } }
   
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

    public GameMode GameMode { get => gameMode; set => gameMode = value; }

    private Action onNextTurn;

    private GameMode gameMode;

    private void Awake()
    {
        SetUpSingleton();
        SetUpControllers();
        SetGameModeBasedOnScene();
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
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            GameMode = new PlayMode();
            levelController.BuildTestLevel();
        }
        else if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            GameMode = new LevelEditorMode();
        }
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
    public void ClickNode(Node n, Node.Direction dir, int button)
    {
        GameMode.OnNodeClick(n, dir, button);
    }
    
    private void SetUpControllers()
    {
        levelController = FindObjectOfType<LevelController>();
        if(levelController != null)
        {
            levelController.RegisterToLevelCreated(CurrentLevelActive);
        }
        movementController = FindObjectOfType<MovementController>();
        levelDesignController = FindObjectOfType<LevelDesignController>();
        smoothGraphics = FindObjectOfType<SmoothGraphics>();
        cameraController = FindObjectOfType<CameraController>();
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




}
