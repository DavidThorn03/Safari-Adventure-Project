using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameManager gm;
    public float speed;
    private float lowerBounds = -10;
    private Animator enemyAnim;
    public ParticleSystem dirt;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyAnim = GetComponent<Animator>();
        dirt.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.score < 100){
            enemyAnim.SetFloat("Speed_f", 1 + (gm.score / 30));
            speed = (gm.playerSpeed + 5) + (gm.score / 5);
        }
        else{
            enemyAnim.SetFloat("Speed_f", 34);
            speed = gm.playerSpeed + 25;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
        if (transform.position.z < lowerBounds)
        {
            Destroy(gameObject);
            gm.UpdateScore(1);
        }
    }

}
