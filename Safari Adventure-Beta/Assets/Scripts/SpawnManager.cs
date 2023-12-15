using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //objects to spawn
    public GameObject enemy;
    public GameObject[] powerups;

    private float spawnRangex = 10;
    private float spawnPosz = 80;
    private float enemystartDelay = 1.5f;
    private float powerstartDelay = 5;
    private float spawnInterval;

    //game manager script to use game manager methods and variables
    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();//set game manager
        //start spawning enemys and power ups randomly
        Invoke("SpawnEnemy", enemystartDelay);
        Invoke("SpawnPowerUp", powerstartDelay);
    }

    void Update()
    {
    
    }

    void SpawnEnemy()
    {
        if(gm.isGameActive)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangex, spawnRangex), 0, spawnPosz);//create random spawn position on x axis
            Instantiate(enemy, spawnPos, enemy.transform.rotation);//spawn
        }
        if(gm.score < 100)
        {
            spawnInterval = Random.Range(1.5f / (1 + (gm.score / 5)), 3.5f / (1 + (gm.score / 5)));//spawn enemys faster as score increases
        }
        else{
            spawnInterval = Random.Range(0.075f , 0.175f);//when score is over 100, dont increase spawn rate further
        }
        Invoke("SpawnEnemy", spawnInterval);//recall method to restart proccess
    }
    void SpawnPowerUp()
    {
        if(gm.isGameActive)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangex, spawnRangex), 2, spawnPosz);//create random spawn position on x axis
            int i = Random.Range(0, 2);//randomly select which powerup is spawned
            Instantiate(powerups[i], spawnPos, enemy.transform.rotation);//spawn
        }
        spawnInterval = Random.Range(10, 20);//randomly spawn time between 10 and 20 seconds
        Invoke("SpawnPowerUp", spawnInterval);//recall method to restart proccess
    }

}