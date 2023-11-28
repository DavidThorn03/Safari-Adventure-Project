using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public GameObject gameOver;
    public TextMeshProUGUI scoreText;   
    public int score;

    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyPrefab.transform.position.z < -5)
        {
            UpdateScore(1);
        }
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        gameOver.SetActive(false);
        isGameActive = true;
    }
    public void Home(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
