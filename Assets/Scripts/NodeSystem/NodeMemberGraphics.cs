using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMemberGraphic : MonoBehaviour
{
    private Node node;
    private Animator animator;
    public Node Node { get => node; set => node = value; }
    public Animator Animator { get => animator;  }

    private void Awake() {
        GameController.Game.RegisterForGameStateChanged(OnGameStateChange);
    }
    private void Start() {
        if(gameObject.GetComponent<Animator>() != null) {
            animator = gameObject.GetComponent<Animator>();
        }
        GameController.Game.RegisterForGameStateChanged(OnGameStateChange);
    }
    private void OnGameStateChange() {
        if (GetComponent<Collider>() != null) {
            Collider col = GetComponent<Collider>();
            if (GameController.Game.CurrentGameState is LevelEditorMode) {
                col.enabled = true;
            }
            else {

                col.enabled = false;
            }
        }
    }
    public void MoveAnimation() {
        if(animator != null) {
            animator.SetBool("isWalking", true);
        }
        
    }
    public void StillAnimation() {
        if (animator != null) {
            animator.SetBool("isWalking", false);
        }
    }
}
