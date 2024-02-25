using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public int currentHealth;
    public int blockCount = 4;
    public bool isBlocking = false;
    public bool isAlive = true;
    [SerializeField] Animator animator;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
            Debug.Log("death");
            rb.useGravity = false;
            rb.freezeRotation = true;
            rb.isKinematic = true;
            animator.SetTrigger("Death");
            animator.GetComponent<CapsuleCollider>().enabled = false;
        }

    }
}
