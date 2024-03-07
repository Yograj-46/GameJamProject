using UnityEngine;

public class Enemy_Walk : StateMachineBehaviour
{
    float timer;
    float randomTime;
    Rigidbody rb;
    public float walkingSpeed;

    public Vector3 randomPosition;
    int randomPoint;

    //walk to chase
    float attackRange = 7.5f;
    Transform player;
    PlayerHealth playerHealth;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        randomTime = Random.Range(7.5f, 20f);
        rb = animator.GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();

        randomPoint = Random.Range(0, 4);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (randomPoint)
        {
            case 0:
                randomPosition = new Vector3(animator.transform.position.x + 10, 0, animator.transform.position.z);
                break;
            case 1:
                randomPosition = new Vector3(animator.transform.position.x - 10, 0, animator.transform.position.z);
                break;
            case 2:
                randomPosition = new Vector3(animator.transform.position.x, 0, animator.transform.position.z + 10);
                break;
            case 3:
                randomPosition = new Vector3(animator.transform.position.x, 0, animator.transform.position.z - 10);
                break;
        }
        rb.MovePosition(Vector3.MoveTowards(rb.transform.position, randomPosition, walkingSpeed * Time.deltaTime));

        Vector3 targetDirection = randomPosition - rb.transform.position;
        targetDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 5f * Time.deltaTime));

        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            animator.SetBool("isRoaming", false);
        }

        if(Vector3.Distance(rb.transform.position, player.transform.position)<=attackRange && playerHealth.isAlive)
        {
            animator.SetBool("isRoaming", false);

        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.MovePosition(rb.transform.position);
    }


}
