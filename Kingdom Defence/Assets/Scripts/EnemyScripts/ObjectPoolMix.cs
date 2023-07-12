using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMix : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;
    [SerializeField] [Range(0,50)] int poolSize = 5;
    [SerializeField] [Range(0.1f,30f)] float spawnDelay = 2f;

    GameObject[] pool;
    

    void Awake()
    {
        PopulatePool();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        

        for (int i = 0; i < pool.Length; i++)
        {
            int idx = (i + 1) % 2;
            pool[i] = Instantiate(enemy[idx], transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectPool()
    {
        for(int i = 0; i < poolSize; i++){
            if(!pool[i].activeInHierarchy){
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectPool();
            yield return new WaitForSeconds(spawnDelay);
        }

    }
}
