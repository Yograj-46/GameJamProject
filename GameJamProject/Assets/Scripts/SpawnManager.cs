using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject orbs;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateOrbs", 2.5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOrbs();
        StartCoroutine("DestroyOrbs");
    }
    void InstantiateOrbs(){
        Instantiate(orbs, GenerateRandomPosition(), orbs.transform.rotation);
    }

    private Vector3 GenerateRandomPosition(){

        float xPosition = Random.Range(180, 220);
        float zPosition = Random.Range(180, 210);

        Vector3 randomPos = new (xPosition, 2.5f, zPosition);
        return randomPos;
    }

    IEnumerator DestroyOrbs(){
        yield return new WaitForSeconds(5f);
        Destroy(orbs);
    }
}
