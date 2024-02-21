using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth + "enemyHealth");
        if(currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            animator.GetComponent<CapsuleCollider>().enabled = false;

        }
    }
}
