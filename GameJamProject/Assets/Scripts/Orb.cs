using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    public Transform player;
    public float range;
    public float distance;
    public GameObject summoningButton;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        summoningButton = GameObject.Find("Summoning_Button");
        distance = Vector3.Distance(player.position, transform.position);
        Vector3 target = (player.position - transform.position).normalized;
        
        if(distance <= range){
            //summoningButton.gameObject.SetActive(true); //Enable button when player is close to the orb
            if(Input.GetKey(KeyCode.M)){
                transform.Translate(target * 5 * Time.deltaTime);
            }
        }
        else{
            //summoningButton.gameObject.SetActive(false); //Disable button when player goes far from orb
        }

        StartCoroutine("DestroyItself");
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Player"){
            Destroy(gameObject);
        }
        gameManager.UpdateCount(1);
    }

    IEnumerator DestroyItself(){
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
