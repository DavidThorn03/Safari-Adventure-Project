using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    //game manager script to use game manager methods and variables
    private GameManager gm;
    private float lowerBounds = -10;

    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();//set game manager
    }

    void Update()
    {
        //if object enters lower bounds(leaves screen behind player) destroy it
        if (transform.position.z < lowerBounds)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * gm.playerSpeed);//move forward
    }
}
