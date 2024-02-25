using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Death : StateMachineBehaviour
{
    Rigidbody rb;
    EnemyHealth enemyHealth;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Death");
       // player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        //rb.useGravity = false;
        enemyHealth = animator.GetComponent<EnemyHealth>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyHealth.currentHealth <= 0)
        {
            Debug.Log(enemyHealth.currentHealth);
            Debug.Log("current health" + enemyHealth.currentHealth);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("end");
    }
}
