using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Camera : MonoBehaviour
{
    public Transform player;
    public GameObject cameraConstraint;
    public Vector3 offset;
    public float chasingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + player.TransformVector(offset), chasingSpeed * Time.deltaTime);
        transform.LookAt(player);
    }
}
