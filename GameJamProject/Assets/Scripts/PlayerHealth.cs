using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider damageSlider;
    float speed = 0.5f;
    int maxHealth = 100, minHealth = 1;


    private void Start()
    {
        healthSlider.value = maxHealth;
        healthSlider.maxValue = maxHealth;
        damageSlider.maxValue = maxHealth;
    }

    private void Update()
    {

        if (healthSlider.value != damageSlider.value)
            damageSlider.value -= speed;

        if (healthSlider.value < minHealth)
            PlayerOut();

    }
    public void TakeDamage(int damage)
    {
        healthSlider.value -= damage;
    }
    void PlayerOut()
    {
        // death screen and reload scene
    }
}