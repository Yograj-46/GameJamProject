using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] orbs;
    public Vector3[] spawnPositions;
    public int orbsPerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateOrbs", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InstantiateOrbs(){

        foreach (Vector3 position in spawnPositions)
        {
            for (int i = 0; i < orbsPerSpawn; i++)
            {
                int index = Random.Range(0, orbs.Length);
                Instantiate(orbs[index], position, orbs[index].transform.rotation);
            }
        }
    }
}
