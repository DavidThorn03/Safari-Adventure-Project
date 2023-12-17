using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);//show pause menu
        Time.timeScale = 0f;//freeze game
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);//hide pause menu
        Time.timeScale = 1f;//unfreeze game
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;//unfreeze game
        SceneManager.LoadScene(sceneID);//load scene
    }
}