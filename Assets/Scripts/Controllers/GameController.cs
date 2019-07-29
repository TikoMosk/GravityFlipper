using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _game;
    public static GameController Game{ get { return _game; } }
    public LevelController levelController;
    public Level currentLevel = null;
    public LevelDesignController levelDesignController;
    public MovementController movementController;

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
            gameMode = new PlayMode();
        }
        else if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            gameMode = new LevelEditorMode();
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
        gameMode.OnNodeClick(n, dir, button);
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
    }
    private void CurrentLevelActive()
    {
        currentLevel = levelController.Level;
    }
    public void NextTurn()
    {
        Debug.Log("NEXT TURN");
    }
    public void RegisterForNextTurn(Action onNextTurn)
    {
        this.onNextTurn += onNextTurn;
    }




}
