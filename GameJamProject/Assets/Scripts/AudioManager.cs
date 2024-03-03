using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip heartBeatfx,attack,hit,swordDraw,swordKeep,whip;
    public static AudioManager instance;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance == null)
            instance = this;

    }

    public void clip(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }


}
