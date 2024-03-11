using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSFX;
    public AudioClip selectSFX;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayMode()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void HoverSfx()
    {
        audioSource.PlayOneShot(hoverSFX);
        
        //audioSource.Play();
    }
    public void SelectedMenuSfx()

    {
        audioSource.PlayOneShot(selectSFX);
        
    }
    public void DisableSelectMenuSfx()
    {
       
    }
}
