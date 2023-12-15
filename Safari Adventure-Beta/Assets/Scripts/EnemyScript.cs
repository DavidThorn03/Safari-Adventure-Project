using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //game manager script to use game manager methods and variables
    private GameManager gm;

    public float speed;
    private float lowerBounds = -10;

    private Animator enemyAnim;
    public ParticleSystem dirt;


    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();//set game manager
        enemyAnim = GetComponent<Animator>();//set enemy animator
        dirt.Play();//play dirt effect
    }

    void Update()
    {
        if(gm.score < 100){//move faster as score increases
            enemyAnim.SetFloat("Speed_f", 1 + (gm.score / 30));//changes animation to match movement speed
            speed = (gm.playerSpeed + 5) + (gm.score / 5);
        }
        else{//if score is greater then 100 dont increase speed
            enemyAnim.SetFloat("Speed_f", 34);
            speed = gm.playerSpeed + 25;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);//move forward
        
        //if player passes enemy destroy it and update score
        if (transform.position.z < lowerBounds)
        {
            Destroy(gameObject);
            gm.UpdateScore(1);
        }
    }

}
