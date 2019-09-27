using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : IGameState
{
    public PlayMode()
    {
        Time.timeScale = 1f;
        string path = "level" + LevelDownloader.Instance.LevelId + ".json";
        GameController.Game.LevelController.LoadLevelFromProject(path);
        GameController.Game.CameraController.ResetCamera();
    }

    public void SetInputs(Vector3 camMovementVec)
    {
    }

    public void OnNodeClick(Node n, Node.Direction dir)
    {
        //if (n.NodeGraphic.GetComponent<Lever>() != null)
        //{
        //    n.NodeGraphic.GetComponent<Lever>().TurnTheLever();
        //}
        //else
            GameController.Game.CurrentLevel.Player.Move(n, dir);

    }
    public void Update()
    {
        GameController.Game.CameraController.CameraPositionPlayMode();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            string path = "level" + LevelDownloader.Instance.LevelId + ".json";
            GameController.Game.LevelController.LoadLevelFromProject(path);
        }
    }
}
