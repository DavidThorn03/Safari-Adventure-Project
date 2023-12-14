using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] powerups;
    private float spawnRangex = 10;
    private float spawnPosz = 20;
    private float enemystartDelay = 1.5f;
    private float powerstartDelay = 5;
    private float spawnInterval;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Invoke("SpawnEnemy", enemystartDelay);
        Invoke("SpawnPowerUp", powerstartDelay);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void SpawnEnemy()
    {
        if(gm.isGameActive){
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangex, spawnRangex), 0, spawnPosz);
        Instantiate(enemy, spawnPos, enemy.transform.rotation);
        }
        spawnInterval = Random.Range(1.5f / (1 + (gm.score / 5)), 3.5f / (1 + (gm.score / 5)));
        Invoke("SpawnEnemy", spawnInterval);
    }
    void SpawnPowerUp()
    {
        if(gm.isGameActive){
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangex, spawnRangex), 2, spawnPosz);
        int i = Random.Range(1, 2);
        Instantiate(powerups[i], spawnPos, enemy.transform.rotation);
        }
        spawnInterval = Random.Range(10, 20);
        Invoke("SpawnPowerUp", spawnInterval);
    }

}