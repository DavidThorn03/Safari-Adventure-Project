using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void PlayBtn()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene(0);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void InstBtn(int sceneID)
    {
        Debug.Log("Instructions");
        SceneManager.LoadScene(sceneID);
    }

    public void BackBtn(int sceneID)
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene(sceneID);
    }
    //DT
    public void GitHubBtn()
    {
        Application.OpenURL("https://github.com/DavidThorn03/IMM-Project.git");
    }
}


