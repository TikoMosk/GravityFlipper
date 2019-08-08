using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesignController : MonoBehaviour
{
    public GameObject block;

    public void OnNodeClick(Node n, Node.Direction dir,int button)
    {
        if(button == 0)
        {
            Node placeNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(n, dir);
            GameController.Game.LevelController.Level.SetNode(placeNode.X, placeNode.Y, placeNode.Z, 1);
        }
        if (button == 1)
        {
            n.SetNodeType(0);
        }
        
    }
    public void ChangeMode() {
        Debug.Log("Change Mode, current mode is " + GameController.Game.GameMode);
        GameController.Game.GameMode = new PlayMode();
    }
}
