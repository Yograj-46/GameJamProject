using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Animator enemyAnim;
    public bool isChasing;
    public bool isAttacking;
    private EnemyHealth health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        Dead();
    }

    void Chase(){
        
    }
    
    void Dead(){
        if(health.currentHealth > 0){
            transform.LookAt(target);
        }
    }
}
