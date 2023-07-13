using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    Bank bank;
    GameControll gameControll;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        gameControll = FindObjectOfType<GameControll>();
    }

    public void Reward(){
        if(bank != null)
            bank.Deposit(goldReward);
        
        if(gameControll != null)
            gameControll.IncreaseKillNum();
    }

    public void Penalty(){
        if(bank != null)
            bank.Withdrawl(goldPenalty);

        if(gameControll != null)
            gameControll.DecreaseHeart();
    }

}
