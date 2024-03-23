using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] NavMeshAgent enemy;
    [SerializeField] Transform player;
    [SerializeField] PlayerController playerController;

    public float chasingSpeed;
    public bool isChasing;
    public bool isAttacking;
    public bool isGrounded;
    public float remainingDist;
    private EnemyHealth health;

    //Attack Range
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerMask;

    //Chase Range
    public float currentDistance;
    public float chaseRange = 5;

    //Particle after death
    public ParticleSystem deadParticle;
    void Start() {
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update() {
        currentDistance = Vector3.Distance(player.position, transform.position);
        StartCoroutine("AfterEffects");
    }
    
    public void Attack()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, playerMask);
        foreach (Collider col in colInfo) {
            if (!playerController.isBlocking)
            {
                PlayerHealth.TakeDamage(5);
            }
            else if (playerController.isBlocking)
            {
                PlayerHealth.TakeDamage(10);
            }
        }
    }
    void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name == "Ground"){
            isGrounded = true;
        }
    }

    IEnumerator AfterEffects() {
        if(!health.isAlive){
            yield return new WaitForSeconds(2.5f);
            deadParticle.gameObject.SetActive(true);
            yield return new WaitForSeconds(10.0f);
            Destroy(gameObject);
        }
    }
}