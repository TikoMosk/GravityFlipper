using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitScript : MonoBehaviour
{
    private EnemySpiderState state;

    private Node destNode;
    private Node currentNode;

    private void Start()
    {
        state = new IdleState();
    }

    public void StartPursuit()
    {
        destNode = GameController.Game.CurrentLevel.Player.NodeObjectGraphic.Node;
        GetComponent<Animator>().SetBool("Chasing", true);
    }

    public void EndPursuit()
    {
        Debug.Log("EndPursuit");
        GetComponent<Animator>().SetBool("Chasing", false);
    }

    public void ChangeState(EnemySpiderState newState)
    {
        this.state = newState;
    }

    private void Check()
    {
        if (GameController.Game.CurrentLevel.GetNode((int)vector.x, (int)vector.y, (int)vector.z).NodeMember != null)
        {
            if (GameController.Game.CurrentLevel.GetNode((int)vector.x, (int)vector.y, (int)vector.z).NodeMember.Id == 1)
            {
                StartPursuit();
            }
        }
        state.Chase();
    }

    private bool CheckPlayerPosition()
    {
        currentNode = GameController.Game.CurrentLevel.GetNode((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);

        GameController.Game.CurrentLevel.GetNodeInTheDirection(currentNode, Node.Direction.FORWARD);
        return false;
    }
}

public abstract class EnemySpiderState
{
    public abstract void Chase();
}

public class PursuitState : EnemySpiderState
{
    public override void Chase()
    {
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
        destNode = GameController.Game.CurrentLevel.Player.NodeObjectGraphic.Node;
    }
}

public class IdleState : EnemySpiderState
{
    public override void Chase() { }
}