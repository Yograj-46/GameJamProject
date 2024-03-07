using System.Threading;
using UnityEngine;

public class Enemy_Idle : StateMachineBehaviour
{
    public Transform player;
    public float timer;
    private float randomTime;
    Rigidbody rb;
    EnemyHealth enemyHealth;
    Enemy enemy;
    PlayerHealth playerHealth;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        randomTime = Random.Range(1.5f, 5f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        enemyHealth = animator.GetComponent<EnemyHealth>();
        enemy = animator.GetComponent<Enemy>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer > randomTime){
            animator.SetBool("isRoaming", true);
        }
        
        if (enemyHealth.isAlive && playerHealth.isAlive)
        {
            if (Vector3.Distance(player.position, rb.transform.position) <= enemy.chaseRange )
            {
                animator.SetTrigger("IsChasing");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("IsChasing");

    }


}
