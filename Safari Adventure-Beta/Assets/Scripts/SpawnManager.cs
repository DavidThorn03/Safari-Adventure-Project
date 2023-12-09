using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    private float spawnRangex = 10;
    private float spawnPosz = 20;
    private float startDelay = 1.5f;
    private float spawnInterval;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Invoke("SpawnEnemy", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void SpawnEnemy()
    {
        if(gm.isGameActive){
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangex, spawnRangex), 2, spawnPosz);
        Instantiate(enemy, spawnPos, enemy.transform.rotation);
        }
        spawnInterval = Random.Range(1.5f / (1 + (gm.score / 5)), 3.5f / (1 + (gm.score / 5)));
        Invoke("SpawnEnemy", spawnInterval);
    }
}