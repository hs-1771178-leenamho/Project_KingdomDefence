using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
    [SerializeField] float buildDealy = 1f;

    void Start() {
        StartCoroutine(Build());    
    }
    public bool CreateTower(Tower tower, Vector3 spawnTransform)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) return false;

        if (bank.curBalance >= cost)
        {
            Instantiate(tower.gameObject, spawnTransform, Quaternion.identity);
            bank.Withdrawl(cost);
            return true;
        }

        return false;
    }

    IEnumerator Build(){
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child){
                grandChild.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);
        }

        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDealy);
            foreach(Transform grandChild in child){
                grandChild.gameObject.SetActive(true);
            }
            
        }
        

    }
}
