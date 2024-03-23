using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public GameObject potionHand;
    public Animator animator;
    public Rigidbody rb;
    public CapsuleCollider cc;
    public GameObject player;
    public MeshRenderer mesh;
    public GameObject swordHand;
    public Slider greenHealth;
    public Slider redHealth;
    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = false;
        cc.radius = 0.02f;
        cc.isTrigger = true;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickUp(){
        bool posCheckx= (gameObject.transform.position.x)-1< player.transform.position.x && player.transform.position.x < (gameObject.transform.position.x)+1;
       bool posCheckz= (gameObject.transform.position.z)-1< player.transform.position.z &&  player.transform.position.z < (gameObject.transform.position.z) +1;
        if (posCheckx && posCheckz && Input.GetKey(KeyCode.P) && !(swordHand.activeInHierarchy))
        {

            animator.SetTrigger("Picking");
            Invoke("potionHandActive", 1.47f);
            Invoke("increaseHealth", 2.37f);
            Invoke("potionHandInactive", 3.8f);

        }
    }
        
    void increaseHealth(){
        greenHealth.value += 5;
    }
    void potionHandActive(){
        potionHand.SetActive(true);
        Destroy(mesh);
    }

    void potionHandInactive(){
        potionHand.SetActive(false);
        Destroy(gameObject);
    }
}