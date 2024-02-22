using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider damageSlider;
    float hitpoint = 10, speed = 0.5f;
    int maxHealth = 200, minHealth = 1;


    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        damageSlider = GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;
        damageSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            healthSlider.value -= hitpoint;
        }
    }
    private void Update()
    {

        if (healthSlider.value != damageSlider.value)
            damageSlider.value -= speed;

        if (healthSlider.value < minHealth)
            PlayerWin();
    }
    void PlayerWin()
    {
        Debug.Log("Win");//temp func.update what to do if enemy died
    }
}
