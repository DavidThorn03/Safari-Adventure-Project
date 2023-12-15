using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private int upperBound = 75;
    private int speed = 10;

    //explosion effect
    public ParticleSystem explosion;

    //game manager script to use game manager methods and variables
    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();//set game manager
    }


    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);//move forward over time
        //if object reaches upper bound (leaves view) destroy is
        if(transform.position.z > upperBound){
            Destroy(gameObject);
        }
    }
    //wait 0.3 seconds before destroying rocket so particle effect can play
    IEnumerator explosionCountdown()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision){  
        if(collision.gameObject.CompareTag("Enemy")){//if enemy is hit
            explosion.Play();//play explosion
            gm.UpdateScore(1);//update score when enemy destroyed
            Destroy(collision.gameObject);
            StartCoroutine(explosionCountdown());//start countdown
        }

    }
}