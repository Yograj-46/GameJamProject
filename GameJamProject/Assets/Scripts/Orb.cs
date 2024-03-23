using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    public Transform player;
    public float range;
    public float distance;
    public float speed;

    public void MoveToPlayer(){
        distance = Vector3.Distance(player.position, transform.position);
        if(distance <= range && PlayerController.summoning){
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Player"){
            Destroy(gameObject);
            PlayerController.summoning = false;
            PlayerController.playerAnim.SetTrigger("ReturnToIdle");
            GameManager.UpdateOrbCount(1);
        }
    }
}
