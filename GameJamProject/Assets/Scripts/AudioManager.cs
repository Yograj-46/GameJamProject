using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip heartBeatfx,attack,hit;
    public static AudioManager instance;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance == null)
            instance = this;

    }

    private void Update()
    {
        /*
        if(healthSlider.value<=12)
        {
            audioSource.clip = heartBeatfx;
            audioSource.Play();
        }
        */
    }

    public void lightAttackSFX()
    {
        audioSource.clip = attack;
        audioSource.loop = false;
        audioSource.Play();
    }


}
