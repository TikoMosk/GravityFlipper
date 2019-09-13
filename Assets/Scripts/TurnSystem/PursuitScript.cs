using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitScript : MonoBehaviour
{
    private EnemySpiderState state;

    private Node currentNode;
    private Node destNode;
    private Vector3 nextPlatform;

    private void Awake()
    {
        EventController.currentInstance.Register(Check);
    }

    private void Start()
    {
        state = new IdleState();
        state.enemy = this.gameObject;
        UpdateCurrentNode();
    }

    public void ChangeState(EnemySpiderState newState)
    {
        this.state = newState;
        state.enemy = this.gameObject;
    }

    private void UpdateCurrentNode()
    {
        currentNode = GameController.Game.CurrentLevel.GetNode((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
    }

    private void UpdateDestNode()
    {
        destNode = GameController.Game.CurrentLevel.Player.Graphic.Node;
    }

    private void Check()
    {
        if (state is IdleState)
        {
            if (state.IsPlayerNear())
            {
                ChangeState(new PursuitState());
                state.StartPursuit();
                UpdateDestNode();
            }
        }
        else
        {
            UpdateCurrentNode();

            nextPlatform = destNode.GetPosition() - Vector3.up;

            if (Vector3.Distance(currentNode.GetPosition(), destNode.GetPosition()) > 1.0f
                || transform.GetChild(6).position == destNode.GetPosition()
                || destNode == null
                || destNode.Id != 0
                || GameController.Game.CurrentLevel.GetNode(nextPlatform).Id == 4)
            {
                state.EndPursuit();
                ChangeState(new IdleState());
                return;
            }
            else
            {
                state.Chase(currentNode, destNode);
                currentNode = destNode;
                UpdateDestNode();
            }
        }
    }

    private void OnDestroy()
    {
        EventController.currentInstance.Remove(Check);
    }
}

public abstract class EnemySpiderState
{
    protected internal GameObject enemy;

    public abstract void StartPursuit();

    public abstract void EndPursuit();

    public abstract void Chase(Node currentNode, Node destNode);

    public virtual bool IsPlayerNear()
    {
        Transform[] sides = enemy.GetComponentsInChildren<Transform>();
        Node nextNode;
        foreach (Transform side in sides)
        {
            if (side.gameObject.tag == "Side")
            {
                nextNode = GameController.Game.CurrentLevel.GetNode(side.position);
                if (nextNode != null && nextNode.NodeMember != null)
                {
                    if (nextNode.NodeMember.Id == 1)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}

public class PursuitState : EnemySpiderState
{
    public override void Chase(Node currentNode, Node destNode)
    {
        GameController.Game.CurrentLevel.MoveObject(currentNode, destNode);
    }

    public override void EndPursuit()
    {
        enemy.GetComponent<Animator>().SetBool("Chasing", false);
    }

    public override void StartPursuit()
    {
        enemy.GetComponent<Animator>().SetBool("Chasing", true);
    }
}

public class IdleState : EnemySpiderState
{
    public override void Chase(Node currentNode, Node destNode) { }

    public override void EndPursuit() { }

    public override void StartPursuit() { }
}
