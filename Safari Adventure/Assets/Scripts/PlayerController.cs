using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float speed = 10.0f;
    private float xRange = 10;
    private Rigidbody playerRB;
    private float jumpForce = 600;
    public bool isOnGround = true;
    private GameManager gm;
    
    //AD
    private bool hasDestroyAbility = false;
    

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    void Update()
    {
            if(transform.position.x < -xRange){
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
            if(transform.position.x > xRange){
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }
            if(isOnGround && gm.isGameActive){
                horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
            }
            if(isOnGround && gm.isGameActive && Input.GetKeyDown(KeyCode.UpArrow)){
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
            
            //AD
        if (hasDestroyAbility && Input.GetKeyDown(KeyCode.Space))
        {
            DestroyObjects();
        }
    }

    private void OnCollisionEnter(Collision collision){    
        if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
        }
        else if(collision.gameObject.CompareTag("Enemy")){
            Debug.Log("GameOver");
            gm.GameOver();
        }
    }

        //AD
    public void EnableDestroyAbility()
    {
        hasDestroyAbility = true;
    }
    private void DestroyObjects()
    {
        Destroy(GameObject.FindWithTag("Enemy"));
    }
   
}
