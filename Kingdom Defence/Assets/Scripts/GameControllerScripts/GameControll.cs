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
    [SerializeField] AudioSource BackgroundSound;
    [SerializeField] AudioSource AnimalSound;
    [SerializeField] AudioSource ClearSound;
    [SerializeField] AudioSource FailSound;
    [SerializeField] AudioSource ClickSound;
    

    int killNum;
    int heartNum;
    int heartIdx;


    void Start()
    {
        Time.timeScale = 1;
        killNum = 0;
        heartNum = 3;
        heartIdx = 0;
        

        kiiTextUI.text = killNum + " / " + gameClearKillNum;
        if (GameClearCanvas != null) GameClearCanvas.gameObject.SetActive(false);
        if (GameOverCanvas != null) GameOverCanvas.gameObject.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DecreaseHeart();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            IncreaseKillNum();
        }
    }

    public void IncreaseKillNum()
    {
        killNum++;
        kiiTextUI.text = killNum + " / " + gameClearKillNum;

        if (killNum >= gameClearKillNum)
        {
            PopOutClearCanvas();
        }

    }

    public void DecreaseHeart()
    {
        heartNum--;

        if (heartIdx >= HeartArr.Length) return;

        if (HeartArr[heartIdx] != null) HeartArr[heartIdx].SetActive(false);

        heartIdx++;

        if (heartNum <= 0)
        {
            // 타임스탑을 걸고 게임 오버 캔버스를 띄워 
            // 리스타트 하던가 게임 종료 하던가 택1 할 수 있도록
            PopOutOverCanvas();
        }

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadToNextLevel());
    }

    IEnumerator LoadToNextLevel(){
        ClickSound.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        Scene curScene = SceneManager.GetActiveScene();
        int sceneAmount = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = curScene.buildIndex + 1;
        if (sceneAmount > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }



    public void ReloadLevel()
    {
        StartCoroutine(ReloadCurLevel());
    }

    IEnumerator ReloadCurLevel()
    {
        ClickSound.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void PopOutClearCanvas()
    {
        Time.timeScale = 0;

        GameClearCanvas.gameObject.SetActive(true);
        if (BackgroundSound.isPlaying) BackgroundSound.Pause();
        if (AnimalSound.isPlaying) AnimalSound.Pause();
        if (!ClearSound.isPlaying)
        {
            ClearSound.Play();
        }
    }

    void PopOutOverCanvas()
    {
        Time.timeScale = 0;
        GameOverCanvas.gameObject.SetActive(true);
        if (BackgroundSound.isPlaying) BackgroundSound.Pause();
        if (AnimalSound.isPlaying) AnimalSound.Pause();
        if (!FailSound.isPlaying)
        {
            FailSound.Play();
        }

    }


}
