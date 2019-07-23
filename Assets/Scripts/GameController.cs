using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _game;
    public static GameController Game{ get { return _game; } }
    public LevelController levelController;
    public MovementController movementController;

    private void Awake()
    {
        SetUpSingleton();
        SetUpControllers();
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
    private void SetUpControllers()
    {
        levelController = FindObjectOfType<LevelController>();
        movementController = FindObjectOfType<MovementController>();
    }
    
}
