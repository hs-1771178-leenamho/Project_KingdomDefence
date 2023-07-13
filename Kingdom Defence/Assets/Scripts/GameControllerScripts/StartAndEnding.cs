using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAndEnding : MonoBehaviour
{
    
    public void StartGame(){
        SceneManager.LoadScene(1);

    }   

    public void RestartGame(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    } 
}
