using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    //[SerializeField] NavMeshAgent enemy;
    [SerializeField] Animator enemyAnim;
    [SerializeField] Transform player;
    //public ThirdPersonCharacter character;
    
    public float chasingSpeed;
    public bool isChasing;
    public bool isAttacking;
    public float remainingDist;
    private EnemyHealth health;
    // Start is called before the first frame update
    void Start()
    {
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

    void Chase(){

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
   
    void Attack(){
        if(transform.position.z == player.position.z){
            isAttacking = true;
        }
        else{
            isAttacking = false;
        }
        if(isAttacking){
            enemyAnim.SetTrigger("Attack1");
        }
    }
}
