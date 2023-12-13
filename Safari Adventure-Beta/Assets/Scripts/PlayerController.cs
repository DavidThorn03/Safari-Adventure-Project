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
    private Animator playerAnim;
    public bool StrPower = false;
    public bool GunPower = false; 
    public GameObject powerupIndicator;
    
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAnim = GetComponent<Animator>();
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
            playerAnim.SetBool("Jump_b", false);
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
        if(isOnGround && gm.isGameActive && Input.GetKeyDown(KeyCode.UpArrow)){
            playerAnim.SetBool("Jump_b", true);
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        powerupIndicator.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
        powerupIndicator.transform.Rotate(0f, 0.5f, 0f);
    }


    private void OnCollisionEnter(Collision collision){  
        if(collision.gameObject.CompareTag("StrPower")){
            Destroy(collision.gameObject);
            StrPower = true;
            GunPower = false;
            gm.playerSpeed = 10;
            playerAnim.SetFloat("Speed_f", 1f);
            powerupIndicator.gameObject.SetActive(true);
            PowerupCountdownRoutine();
        }

        else if(collision.gameObject.CompareTag("GunPower")){
            Destroy(collision.gameObject);
            Debug.Log("hit it");
            StrPower = false;
            GunPower = true;
            powerupIndicator.gameObject.SetActive(true);
            PowerupCountdownRoutine();
        }

        else if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
        }

        else if(collision.gameObject.CompareTag("Enemy")){
            if(!StrPower){
                Debug.Log("GameOver");
                gm.GameOver();
                if(playerAnim.GetBool("Jump_b")){
                    playerAnim.SetInteger("DeathType_int", 2);
                }
                playerAnim.SetBool("Death_b", true);
            }
            else{
                Destroy(collision.gameObject);
                gm.UpdateScore(1);
            }
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerupIndicator.gameObject.SetActive(false);
        playerAnim.SetFloat("Speed_f", 0.5f);
        gm.playerSpeed = 5;
        StrPower = false;
        GunPower = false;
    }
}
