using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public static int currentHealth;
    public int blockCount = 4;
    public bool isBlocking = false;
    public bool isAlive = true;
    [SerializeField] Animator animator;
    Rigidbody rb;

    [Header ("Enemy Health Bar Reference")]
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth); //Max health at starting
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        SetHealth(currentHealth); //Current health of enemy
        blockCount--;
        if (currentHealth <= 0)
        {
            isAlive = false;
            healthSlider.gameObject.SetActive(false);
            Debug.Log("death");
            rb.useGravity = false;
            rb.freezeRotation = true;
            rb.isKinematic = true;
            animator.SetTrigger("Death");
            animator.GetComponent<CapsuleCollider>().enabled = false; 
        }
    }

    //To set maximum value of the slider
    public void SetMaxHealth(float health){
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    
    //To set current health  of the enemy in slider
    public void SetHealth(int health){
        healthSlider.value = health;
    }
    public void DisableObject(){
        currentHealth = maxHealth;
        gameObject.SetActive(false);
    }
}
