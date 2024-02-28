using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    public float speed = 5f;
    public float attackRange;
    public Transform player;
    Rigidbody rb;
    public float rotationSpeed = 5f;
    PlayerHealth playerHealth;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        rb = animator.GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyHealth enemyHealth = animator.GetComponent<EnemyHealth>();
        if (enemyHealth.isAlive && playerHealth.isAlive)
        {
            Vector3 targetDirection = player.position - rb.transform.position;
            targetDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));

            Vector3 targetPosition = player.position;
            targetPosition.y = rb.transform.position.y;

            rb.MovePosition(Vector3.MoveTowards(rb.transform.position, targetPosition, speed * Time.deltaTime));
            if (Vector3.Distance(targetPosition, rb.transform.position) <= attackRange)
            {
                animator.SetTrigger("Attack1");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack1");
    }
}
