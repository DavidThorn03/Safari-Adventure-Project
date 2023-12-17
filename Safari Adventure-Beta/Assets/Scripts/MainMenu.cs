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
        SceneManager.LoadScene(0);//load scene
        pauseMenu.SetActive(false);//hide pause menu
        Time.timeScale = 1f;//unfreeze game
    }

    public void InstBtn(int sceneID)
    {
        Debug.Log("Instructions");
        SceneManager.LoadScene(sceneID);//load scene
    }

    public void BackBtn(int sceneID)
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene(sceneID);//load scene
    }
    public void GitHubBtn()
    {
        Application.OpenURL("https://github.com/DavidThorn03/IMM-Project-Beta.git");//open github repo
    }
}