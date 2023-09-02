using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseGame();
        }
    }

    void pauseGame(){
        Time.timeScale = 0f;
        if(playerUI != null) playerUI.gameObject.SetActive(false);
        if(pauseUI != null) pauseUI.gameObject.SetActive(true);
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        if(playerUI != null) playerUI.gameObject.SetActive(true);
        if(pauseUI != null) pauseUI.gameObject.SetActive(false);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
