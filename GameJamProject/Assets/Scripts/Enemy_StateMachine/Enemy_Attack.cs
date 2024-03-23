using UnityEngine;

public class Enemy_Attack : StateMachineBehaviour
{
    public Transform player;
    Rigidbody rb;
    PlayerHealth playerHealth;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (Vector3.Distance(player.position, rb.transform.position) >= 2 && PlayerHealth.isAlive)
        {
            animator.SetTrigger("BackToRun");
        }
        if (!PlayerHealth.isAlive)
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        animator.ResetTrigger("BackToRun");
        animator.ResetTrigger("Idle");
    }
}
