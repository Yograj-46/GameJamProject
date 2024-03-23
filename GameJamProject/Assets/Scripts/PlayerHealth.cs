using Cinemachine.PostFX;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour
{
    public static ParticleSystem bloodSplash;
    public static Slider healthSlider;
    public static Slider damageSlider;
    public GameObject healthCircle;
    float speed = 0.5f;
    static int maxHealth = 100, minHealth = 1;
    static Animator animator;
    public static bool isAlive = true;

    [SerializeField] Volume volume;

    
    private void Start(){
        GetObjects();

        bloodSplash.Stop();
        healthSlider.value = maxHealth;
        healthSlider.maxValue = maxHealth;
        damageSlider.maxValue = maxHealth;
        animator = GetComponent<Animator>();
    }

    void GetObjects(){
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        damageSlider = GameObject.Find("DamageSlider").GetComponent<Slider>();
        bloodSplash = GameObject.Find("BloodSplash").GetComponent<ParticleSystem>();
    }

    void bloodStop(){
        bloodSplash.Stop();
    }

    private void Update(){
        UpdateDamage();

        bool isLowhealth = healthSlider.value <= 12;
        if (healthSlider.value < minHealth)
            PlayerOut();
    }

    private void UpdateDamage(){
        if (healthSlider.value < damageSlider.value)
            damageSlider.value -= speed;
        if (healthSlider.value > damageSlider.value)
            damageSlider.value += speed;

        if (healthSlider.value < 20) {
            volume.enabled = true;
            if(!AudioManager.instance.audioSource.isPlaying)
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.heartBeatfx);
        }
        else volume.enabled = false;
    }

    public static void TakeDamage(int damage){
        healthSlider.value -= damage;
        if(healthSlider.value > minHealth) {
            animator.SetTrigger("Hit");
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.getHit);
            int rand = random.Range(1, 3);
            if (rand == 1)
            {
                var bs = new PlayerHealth(); //Var bs denotes bloodStop roughly
                if(bloodSplash != null) bloodSplash.Play();
                bs.Invoke("bloodStop", 0.25f);
            }
        }
    }

    void PlayerOut(){
        // death screen and reload scene
        if(healthSlider.value == 0){
            isAlive = false;
            volume.enabled = false;
            animator.SetTrigger("Death");
            AudioManager.instance.audioSource.Stop();
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