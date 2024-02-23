using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Buoancy : MonoBehaviour
{
    public float floatStrength;
    
    void OnTriggerStay(Collider other){
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //Calculate buoyancy force based on depth
                float depth = Mathf.Abs(transform.position.y - other.transform.position.y);
                float buoyancyForce = floatStrength * depth;

                //Apply upward Force
                rb.AddForce(Vector3.up * buoyancyForce);
            }
        }
    }
}
