using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbManager : MonoBehaviour
{
    Transform player;
    public GameObject summoningButton;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        summoningButton.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        foreach (Transform orbTransform in transform)
        {
            float distance = Vector3.Distance(orbTransform.position, player.position);
            if(distance <= range){
                Debug.Log("Orb Transform: " + orbTransform.name);
                ActivateSummoningButton();
                return;
            }
            else{
                DeactivateSummoningButton();
            }
        }
    }

    void ActivateSummoningButton(){
        summoningButton.SetActive(true);
    }
    void DeactivateSummoningButton(){
        summoningButton.SetActive(false);
    }
}
