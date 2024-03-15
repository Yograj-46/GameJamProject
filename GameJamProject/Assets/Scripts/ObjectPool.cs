using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    Vector3[] enemyPosition;
    [SerializeField] Transform player;
    [SerializeField] GameObject minotaur;
    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());

    }
    void PopulatePool()
    {
        pool = new GameObject[5];
        enemyPosition = new Vector3[pool.Length];

        for (int i = 0; i < pool.Length; i++)
        {
            int postion = UnityEngine.Random.Range(20, 30);
            enemyPosition[i] = new Vector3(player.position.x + postion, player.position.y, player.position.z + postion);
            pool[i] = Instantiate(minotaur, transform);
            pool[i].SetActive(false);
        }

    }
    void ObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = enemyPosition[i];
                pool[i].SetActive(true);
                return;
            }
        }
    }
    IEnumerator SpawnEnemy()
    {
        ObjectInPool();
        yield return new WaitForSeconds(5);
        StartCoroutine(SpawnEnemy());
    }
    
}
