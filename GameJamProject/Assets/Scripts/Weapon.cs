using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    PlayerController controller;
    public bool isEquiped = false;
    void Start(){
        controller = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player")
        {
            isEquiped=true;
            gameObject.SetActive(false);
        }
    }
    
   
}
