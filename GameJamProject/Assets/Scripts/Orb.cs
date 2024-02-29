using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    public Transform player;
    public float range;
    public float distance;
    public Button summoningButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        distance = Vector3.Distance(player.position, transform.position);
        if(distance <= range){
            summoningButton.gameObject.SetActive(true); //Enable button when player is close to the orb
        }
        else{
            summoningButton.gameObject.SetActive(false); //Disable button when player goes far from orb
        }

    }

    public void MoveOrb(){
        transform.LookAt(player);
        transform.Translate(Vector3.forward * 2.5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Player"){
            Destroy(gameObject);
        }
    }
}
