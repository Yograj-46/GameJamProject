using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_FlyForward : StateMachineBehaviour
{
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody rb = animator.GetComponent<Rigidbody>();

        Vector3 targetPosition = player.position;
        targetPosition.y = player.position.y;
            
        rb.MovePosition(Vector3.MoveTowards(rb.transform.position, targetPosition, 5f * Time.deltaTime));
        rb.transform.LookAt(player);

        if(Vector3.Distance(player.position, rb.transform.position) < 25f){
            animator.SetTrigger("Land");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            animator.ResetTrigger("Land");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
