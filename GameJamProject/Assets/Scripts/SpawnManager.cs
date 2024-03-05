using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] orbs;
    public GameObject potions;  
    public Vector3[] spawnPositions;
    public int orbsPerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateOrbs", 10f, 10f);
        for(int i = 0;i<6;i++)
        {
            InstantiatePotions();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InstantiatePotions(){

        int ind = Random.Range(0, 9);
        Instantiate(potions, spawnPositions[ind], potions.transform.rotation);
    }

    void InstantiateOrbs()
    {
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
