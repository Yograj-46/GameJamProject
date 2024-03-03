using Cinemachine.PostFX;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
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

    [SerializeField] Volume volume;
    [SerializeField] AudioSource heartBeatSFX;

    
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


    }

    private void UpdateDamage()
    {
        if (healthSlider.value != damageSlider.value)
            damageSlider.value -= speed;

        if (healthSlider.value < 20)
        {
            volume.enabled = true;
            heartBeatSFX.gameObject.SetActive(true);
        }

        else
        {
            volume.enabled = false;
            heartBeatSFX.gameObject.SetActive(false);
        }
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
            volume.enabled = false;
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