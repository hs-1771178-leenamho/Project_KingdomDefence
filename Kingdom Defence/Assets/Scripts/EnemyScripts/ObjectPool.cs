using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
오브젝트 풀이란?
게임을 플레이 할 때, 개체가 생성되고 삭제되는 과정이 반복될수록 볼륨이 큰 게임에선 성능 저하가 쉽게 일어날 수 있음
따라서 각 레벨마다 등장하는 적 개체를 특정 공간에 미리 저장해 플레이어에게 보이지 않게하고 필요할 때마다 꺼내어 쓴다
이때 적 개체가 파괴되거나 기타 다른 이유로 삭제해야할 때 destroy하는 것이 아니라 앞서 만든 공간에 다시 집어넣고
사용자에게 보이지 않게 만든다.
이러한 방식을 채용해 성능 저하를 막고 여러가지 wave 패턴을 추가할 수 있음 
*/
public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnDelay = 2f;

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
            pool[i] = Instantiate(enemy, transform);
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
