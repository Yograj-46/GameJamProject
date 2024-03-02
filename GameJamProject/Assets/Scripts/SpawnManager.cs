using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] orbs;
    public Vector3[] spawnPositions;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateOrbs", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InstantiateOrbs(){
        int index = Random.Range(0, orbs.Length);
        Instantiate(orbs[index], GenerateRandomPosition(), orbs[index].transform.rotation);
    }

    private Vector3 GenerateRandomPosition(){
        Vector3 randomPos = spawnPositions[Random.Range(0, spawnPositions.Length)];
        return randomPos;
    }
}
