using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider damageSlider;
    float hitpoint = 10, speed = 0.5f;
    int maxHealth = 100, minHealth = 1;


    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        damageSlider = GetComponent<Slider>();
        healthSlider.value = maxHealth;
        healthSlider.maxValue = maxHealth;
        damageSlider.maxValue = maxHealth;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Weapon"))
        {
            healthSlider.value -= hitpoint;
        }
    }
    private void Update()
    {

        if (healthSlider.value != damageSlider.value)
            damageSlider.value -= speed;

        if (healthSlider.value < minHealth)
            PlayerOut();
    }
    void PlayerOut()
    {
        Debug.Log("Out");
    }
}
