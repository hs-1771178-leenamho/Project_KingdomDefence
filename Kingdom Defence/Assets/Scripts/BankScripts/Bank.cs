using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 200;
    [SerializeField] int mcurBalance;
    [SerializeField] TextMeshProUGUI textUI;

    public int curBalance{
        get{
            return mcurBalance;
        }
    }

    void Awake() {
        mcurBalance = startingBalance;    
    }

    void Update() {
        textUI.text = "Gold : " + mcurBalance;
    }

    public void Deposit(int amount){
       mcurBalance += Mathf.Abs(amount);
       Debug.Log(mcurBalance);
    }

    public void Withdrawl(int amount){
        mcurBalance -= Mathf.Abs(amount);
        if(mcurBalance < 0) {
           // mcurBalance = 0;
           ReloadLevel();
        }
    }

    void ReloadLevel(){
        Scene cureScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cureScene.buildIndex);
    }
}
