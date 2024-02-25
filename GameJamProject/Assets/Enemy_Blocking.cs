using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Blocking : StateMachineBehaviour
{
    public Transform player;
    Rigidbody rb;
    EnemyHealth enemyHealth;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        enemyHealth = animator.GetComponent<EnemyHealth>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 targetDirection = player.position - rb.transform.position;
        targetDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.deltaTime));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyHealth.blockCount = 3;
        enemyHealth.isBlocking = false;
    }

}
