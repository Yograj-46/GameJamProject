using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public TextMeshProUGUI orbsText;
    private int count;

    void Start(){

    }

    void Update(){
        
    }
    
    public void UpdateCount(int countToAdd){
        count += countToAdd;
        orbsText.text = "Collected Orbs:" + count;
    }
}
