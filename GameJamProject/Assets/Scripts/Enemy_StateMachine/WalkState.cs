using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    Rigidbody rb;
    public float walkingSpeed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        rb = animator.GetComponent<Rigidbody>();
        GameObject wayPoint = GameObject.FindGameObjectWithTag("WayPoint");
        foreach (Transform point in wayPoint.transform)
        {
            wayPoints.Add(point);
        }

        Vector3 wayPointPosition = wayPoints[Random.Range(0, wayPoints.Count)].position;
        rb.MovePosition(Vector3.MoveTowards(rb.transform.position, wayPointPosition, walkingSpeed * Time.deltaTime));
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject wayPoint = GameObject.FindGameObjectWithTag("WayPoint");
        Vector3 wayPointPosition = wayPoints[Random.Range(0, wayPoints.Count)].position;

        Vector3 targetDirection = wayPoint.gameObject.transform.position - rb.transform.position;
            targetDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 5f * Time.deltaTime));

        if(Vector3.Distance(wayPoint.transform.position, rb.transform.position) <= 0f){
            rb.MovePosition(Vector3.MoveTowards(rb.transform.position, wayPointPosition, walkingSpeed * Time.deltaTime));
        }
        timer += Time.deltaTime;
        if(timer > 10f){
            animator.SetBool("isRoaming", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.MovePosition(rb.transform.position);
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
