using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMemberGraphic : MonoBehaviour
{
    private Action<Node.Direction> onClick;
    private Node node;
    private Animator animator;
    public Node Node { get => node; set => node = value; }
    public Animator Animator { get => animator; }

    private void Awake()
    {
    }
    private void Start()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
        GameController.Game.RegisterForGameStateChanged(ColliderUpdate);
        ColliderUpdate();
    }
    private void ColliderUpdate()
    {

        if (GetComponent<Collider>() != null && node.NodeMember.Walkthrough)
        {
            Collider col = GetComponent<Collider>();
            if (GameController.Game.CurrentGameState is LevelEditorMode)
            {
                col.enabled = true;
            }
            else
            {
                col.enabled = false;
            }
        }
    }
    private void OnDestroy()
    {
        GameController.Game.UnRegisterForGameStateChanged(ColliderUpdate);
    }
    public void MoveAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }

    }
    public void StillAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }
    public void GetClicked(Node.Direction dir)
    {
        onClick?.Invoke(dir);
    }
    public void RegisterToClick(Action<Node.Direction> onClick)
    {
        this.onClick += onClick;
    }
}
