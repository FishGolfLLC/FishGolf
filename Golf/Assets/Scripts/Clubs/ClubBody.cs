using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubBody : MonoBehaviour
{

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation(string trigger) {
        animator.SetTrigger(trigger);
    }
}
