using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMemberGraphic : MonoBehaviour
{
    private Node node;
    private Animator animator;
    public Node Node { get => node; set => node = value; }
    public Animator Animator { get => animator;  }

    private void Start() {
        if(gameObject.GetComponent<Animator>() != null) {
            animator = gameObject.GetComponent<Animator>();
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
