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
    Animator animator;


    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        damageSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    private void Update()
    {

        if (healthSlider.value != damageSlider.value)
            damageSlider.value -= speed;

        if (healthSlider.value < minHealth)
        {
            animator.GetComponent<CapsuleCollider>().enabled = false;
            PlayerWin();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            healthSlider.value -= hitpoint;
        }

    }
    void PlayerWin()
    {
        Debug.Log("Win");//temp func.update what to do if enemy died
    }
}
