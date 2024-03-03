using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Particle after death
    public ParticleSystem deadParticle;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        health = GetComponent<EnemyHealth>();
        enemyAnim = GetComponent<Animator>();
        // enemy.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("AfterEffects");
    }
    
    public void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, playerMask);
        foreach (Collider col in colInfo)
        {
            if (!playerController.isBlocking)
            {
                playerHealth.TakeDamage(5);
            }
            else if (playerController.isBlocking)
            {
                playerHealth.TakeDamage(10);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator AfterEffects(){
        if(!health.isAlive){
            yield return new WaitForSeconds(2.5f);
            deadParticle.gameObject.SetActive(true);
            yield return new WaitForSeconds(10.0f);
            Destroy(gameObject);
        }
    }
}