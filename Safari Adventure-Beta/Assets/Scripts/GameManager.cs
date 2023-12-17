using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;//used in other scripts to see if game is running 
    public int score = 0;
    public int playerSpeed = 5;//speed of player/speed ground appears to be moving at

    public TextMeshProUGUI scoreText;//text to show score
    public ParticleSystem dust;

    public GameObject gameOver;//panel when game ends
    public GameObject enemyPrefab;

    void Start()
    {

    }

    void Update()
    {
        //if player pass enemy add one to score
        if(enemyPrefab.transform.position.z < -5)
        {
            UpdateScore(1);
        }
    }
    //when game is over set is game active is false to running actions and show restart panel
    public void GameOver()
    {
        gameOver.SetActive(true);
        isGameActive = false;
        dust.Stop();
    }
    //reset all variables and restart game(for buttons)
    public void RestartGame(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        gameOver.SetActive(false);
        isGameActive = true;
    }
    //transition to diffrent scene
    public void Home(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    //update score
    public void UpdateScore(int scoreToAdd)
    {
        if (isGameActive)
        {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;//change score on screen
        }
    }
}
