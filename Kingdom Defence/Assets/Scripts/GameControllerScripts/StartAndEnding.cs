using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAndEnding : MonoBehaviour
{
    [SerializeField] AudioSource buttonClickSFX;

    public void StartGame(){
        StartCoroutine(LoadLevel(1));
    }   

    public void RestartGame(){
        StartCoroutine(LoadLevel(0));
    }

    public void QuitGame(){
        buttonClickSFX.Play();
        Application.Quit();
    }

    IEnumerator LoadLevel(int idx){
        buttonClickSFX.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(idx);
    }
}
