using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject potionHand;
    public Animator animator;
    public Rigidbody rb;
    public CapsuleCollider cc;
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
}