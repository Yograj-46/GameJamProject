using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Random.Range(5, 15f) * Time.deltaTime);
        transform.Rotate(Vector3.up * Random.Range(0f, 7.5f) * Time.deltaTime);

        Destroy();
    }

    void Destroy(){
        if(transform.position.x >= 750f || transform.position.z >= 750f){
            Destroy(gameObject);
        }
    }
}
