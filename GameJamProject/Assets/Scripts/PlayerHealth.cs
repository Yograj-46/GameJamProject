using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider damageSlider;
    public GameObject healthCircle;
    float speed = 0.5f;
    int maxHealth = 100, minHealth = 1;
    Animator animator;


    private void Start()
    {
        healthSlider.value = maxHealth;
        healthSlider.maxValue = maxHealth;
        damageSlider.maxValue = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (healthSlider.value != damageSlider.value)
            damageSlider.value -= speed;

        if (healthSlider.value < minHealth)
            PlayerOut();

        if (Input.GetKey(KeyCode.P))
            animator.SetTrigger("Picking");

    }
    
    public void TakeDamage(int damage)
    {
        healthSlider.value -= damage;
    }

    void PlayerOut()
    {
        // death screen and reload scene
        if(healthSlider.value == 0){
            animator.SetTrigger("Death");
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("HealingCircle") && healthSlider.value != maxHealth){
            healthSlider.value += maxHealth;
        }
    }
}