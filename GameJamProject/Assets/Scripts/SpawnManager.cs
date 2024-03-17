using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject potions;  
    public Vector3[] spawnPositions;
    public GameObject eagles;
    public int orbsPerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateEagles", 0f, 25f);
        for(int i = 0; i < 10; i++)
        {
            Instantiate(potions, spawnPositions[i], potions.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateEagles(){
        Vector3 spawnPos = new Vector3(Random.Range(150f, 850f), Random.Range(50f, 150f), 75f);

        Instantiate(eagles, spawnPos, eagles.transform.rotation);
    }
}
