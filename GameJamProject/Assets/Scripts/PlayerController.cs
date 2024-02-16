using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Component References")]
    public Animator playerAnim;

    [Header("Inputs")]
    private float horizontalInput;
    private float verticalInput;

    [Header ("Variables")]
    public float speed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start(){
       playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical"); 

        MovePlayer();
        PlayerAnimations();
    }

    void MovePlayer(){
        //To move player
        transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);
        
        //To rotate player
        transform.Rotate(Vector3.up * rotationSpeed * horizontalInput * Time.deltaTime);
    }

    void PlayerAnimations(){
        
    }
}
