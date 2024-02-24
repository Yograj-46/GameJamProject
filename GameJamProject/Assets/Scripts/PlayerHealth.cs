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
        healthSlider.value = maxHealth;
        healthSlider.maxValue = maxHealth;
        damageSlider.maxValue = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("health potion"))
        //    healthSlider.value += 10;
    }
    private void Update()
    {

        if (healthSlider.value != damageSlider.value)
            damageSlider.value -= speed;

        if (healthSlider.value < minHealth)
            PlayerOut();

        if (Input.GetKeyDown(KeyCode.K))
        {
            healthSlider.value -= hitpoint;
        }
    }
    void PlayerOut()
    {
        Debug.Log("Out");
    }
}
