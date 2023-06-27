using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
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
}
