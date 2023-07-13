using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameControll : MonoBehaviour
{
    [SerializeField] int gameClearKillNum = 10;    
    [SerializeField] TextMeshProUGUI kiiTextUI;
    [SerializeField] GameObject[] HeartArr;
    [SerializeField] Canvas GameOverCanvas;
    [SerializeField] Canvas GameClearCanvas;


    int killNum;
    int heartNum;
    int heartIdx;

    void Start(){
        Time.timeScale = 1;
        killNum = 0;
        heartNum = 3;
        heartIdx = 0;

        kiiTextUI.text = killNum + " / " + gameClearKillNum;
        if(GameClearCanvas != null) GameClearCanvas.gameObject.SetActive(false);
        if(GameOverCanvas != null) GameOverCanvas.gameObject.SetActive(false);
        
    }

    public void IncreaseKillNum(){
        killNum++;
        kiiTextUI.text = killNum + " / " + gameClearKillNum;

        if(killNum >= gameClearKillNum){
            PopOutClearCanvas();
        }

    }

    public void DecreaseHeart(){
        heartNum--;
        
        if(heartIdx >= HeartArr.Length) return;

        if(HeartArr[heartIdx] != null) HeartArr[heartIdx].SetActive(false);
        
        heartIdx++;

        if(heartNum <= 0){
            // 타임스탑을 걸고 게임 오버 캔버스를 띄워 
            // 리스타트 하던가 게임 종료 하던가 택1 할 수 있도록
            PopOutOverCanvas();
        }

    }

    public void LoadNextLevel(){
        
        Scene curScene = SceneManager.GetActiveScene();
        int sceneAmount = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = curScene.buildIndex + 1;
        if(sceneAmount > nextSceneIndex){
            SceneManager.LoadScene(nextSceneIndex);
        }
        else{
            SceneManager.LoadScene(0);
        }
        
    }

    public void ReloadLevel(){
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex);
    }

    public void QuitGame(){
        Application.Quit();
    }

    void PopOutClearCanvas(){
        Time.timeScale = 0;
        GameClearCanvas.gameObject.SetActive(true);
    }

    void PopOutOverCanvas(){
        Time.timeScale = 0;
        GameOverCanvas.gameObject.SetActive(true);

    }


}
