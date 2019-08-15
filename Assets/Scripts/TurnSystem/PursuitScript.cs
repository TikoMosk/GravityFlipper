using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitScript : MonoBehaviour
{
    private EnemySpiderState state;

    private Node currentNode;
    private Node destNode;

    private void Start()
    {
        EventController.currentInstance.Register(Check);

        state = new IdleState();
        currentNode = GameController.Game.CurrentLevel.GetNode((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
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
        if (state.IsPlayerNear(currentNode))
        {
            ChangeState(new PursuitState());
        }

        state.Chase(currentNode, ref destNode);
    }
}

public abstract class EnemySpiderState
{
    public abstract void Chase(Node currentNode, ref Node destNode);
    public abstract bool IsPlayerNear(Node node);
}

public class PursuitState : EnemySpiderState
{
    public override void Chase(Node currentNode, ref Node destNode)
    {
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
        destNode = GameController.Game.CurrentLevel.Player.NodeObjectGraphic.Node;
    }

    public override bool IsPlayerNear(Node node) { return false; }
}

public class IdleState : EnemySpiderState
{
    public override void Chase(Node currentNode, ref Node destNode) { }

    public override bool IsPlayerNear(Node node)
    {
        Node nextNode;
        for (int i = 0; i < 6; i++)
        {
            nextNode = GameController.Game.CurrentLevel.GetNodeInTheDirection(node, (Node.Direction)i);
            if (nextNode.NodeMember != null)
            {
                if (nextNode.NodeMember.Id == 1)
                {
                    return true;
                }
            }
        }

        return false;
    }
}