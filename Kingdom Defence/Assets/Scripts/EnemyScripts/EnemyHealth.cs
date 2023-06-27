using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 5;
    [SerializeField] int curHP = 0;

    Enemy enemy;
    // Start is called before the first frame update
    void Start() {
        enemy = FindObjectOfType<Enemy>();
    }
    void OnEnable()
    {
        curHP = maxHP;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        curHP--;
        if (curHP <= 0)
        {
            if(enemy != null) enemy.RewardGold();
            gameObject.SetActive(false);
        }
    }
}
