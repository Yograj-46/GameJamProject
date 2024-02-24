using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Animator enemyAnim;
    [SerializeField] Transform player;
    [SerializeField] CharacterController controller;
    public float chasingSpeed;
    public bool isChasing;
    public bool isAttacking;
    private EnemyHealth health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        enemyAnim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Chase();
        Dead();
    }

    void Chase(){
        enemyAnim.SetBool("Chasing", true);

        Vector3.MoveTowards(transform.position, player.transform.position, chasingSpeed * Time.deltaTime);
    }
    
    void Dead(){
        if(health.currentHealth > 0){
            //transform.LookAt(target);
        }
    }
}
