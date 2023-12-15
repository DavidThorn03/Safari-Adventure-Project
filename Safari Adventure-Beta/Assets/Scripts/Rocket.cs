using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private int upperBound = 75;
    private int speed = 10;
    public ParticleSystem explosion;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if(transform.position.z > upperBound){
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision){  
        if(collision.gameObject.CompareTag("Enemy")){
            explosion.Play();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            gm.UpdateScore(1);
        }

    }
}