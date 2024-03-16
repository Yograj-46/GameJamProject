using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    public Transform player;
    private PlayerController playerController;
    public float range;
    public float distance;
    public float speed;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    public void MoveToPlayer(){
        distance = Vector3.Distance(player.position, transform.position);
        if(distance <= range && playerController.summoning){
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Player"){
            Destroy(gameObject);
            playerController.summoning = false;
            playerController.playerAnim.SetTrigger("ReturnToIdle");
            gameManager.UpdateOrbCount(1);
        }
    }
}
