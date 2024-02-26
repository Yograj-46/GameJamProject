using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    //[SerializeField] NavMeshAgent enemy;
    [SerializeField] Animator enemyAnim;
    [SerializeField] Transform player;
    [SerializeField] PlayerController playerController;
    //public ThirdPersonCharacter character;

    public float chasingSpeed;
    public bool isChasing;
    public bool isAttacking;
    public float remainingDist;
    private EnemyHealth health;

    //Attack Range
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerMask;

    //Chase Range
    public float chaseRange = 5;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        health = GetComponent<EnemyHealth>();
        enemyAnim = GetComponent<Animator>();
        // enemy.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        //IdleToChase();
        Chase();
        //Attack();
    }

    void Chase()
    {

        //if(enemy.remainingDistance > enemy.stoppingDistance){
        //    enemy.SetDestination(player.position);
        //    character.Move(player.position, false, false);
        //}
        //else{
        //    character.Move(Vector3.zero, false, false);
        //}

    }

    //void IdleToChase()
    //{
    //    if (Vector3.Distance(transform.position, player.position) <= chasingRange)
    //        enemyAnim.SetTrigger("IsChasing");


    //    if (Vector3.Distance(transform.position, player.position) <= 1)
    //        enemyAnim.ResetTrigger("IsChasing");
    //}
    
    public void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, playerMask);
        foreach (Collider col in colInfo)
        {
            if (!playerController.isBlocking)
            {
                playerHealth.TakeDamage(10);
                Animator animator = col.GetComponent<Animator>();
                //animator.SetTrigger("LightAttack");
            }
            else if (playerController.isBlocking)
            {
                playerHealth.TakeDamage(4);
                Animator animator = col.GetComponent<Animator>();
                //animator.SetTrigger("LightAttack");
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}