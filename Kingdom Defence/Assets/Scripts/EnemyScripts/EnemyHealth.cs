using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RequireComponet : 명시한 컴포넌트가 게임 객체에도 적용시키게 하는 방법
[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 5;
    [Tooltip("Increase enemy's HP when enemy dies")]
    [SerializeField] int difficultyRamp = 1;
    int curHP = 0;
     

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
            if(enemy != null) enemy.Reward();
            maxHP += difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
