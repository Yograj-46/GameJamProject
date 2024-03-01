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
    public bool isAlive = true;

    public Image overlay;
    public float overlaySpeed=1f;
    float temp_A=0;


    private void Start()
    {
        healthSlider.value = maxHealth;
        healthSlider.maxValue = maxHealth;
        damageSlider.maxValue = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateDamage();

        bool isLowhealth = healthSlider.value <= 12;
        if (healthSlider.value < minHealth)
            PlayerOut();

        if(isLowhealth && isAlive)
        {
            if(temp_A<100)
            {
                temp_A += Time.deltaTime*overlaySpeed;Debug.Log("+");
            }
            else
            {
                temp_A -= Time.deltaTime * overlaySpeed;Debug.Log("-");
            }
            
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, temp_A);
        }
        else
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        }

    }

    private void UpdateDamage()
    {
        if (healthSlider.value != damageSlider.value)
            damageSlider.value -= speed;
    }

    public void TakeDamage(int damage)
    {
        healthSlider.value -= damage;
    }

    void PlayerOut()
    {
        // death screen and reload scene
        if(healthSlider.value == 0){
            isAlive = false;
            animator.SetTrigger("Death");
        }
    }

    private void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("HealingCircle") && healthSlider.value != maxHealth){
            healthSlider.value += speed;
        }
        if(healthSlider.value == maxHealth)
            damageSlider.value = maxHealth;
    }
}