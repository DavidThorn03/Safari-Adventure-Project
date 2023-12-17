using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float speed = 10.0f;//left to right player speed
    private float xRange = 9;//furthest from 0 the player can move on the z axis (left to right)
    private float jumpForce = 600;

    private bool StrPower = false;//has strength power
    private bool GunPower = false;//has gun power
    private bool isOnGround = false;

    //player variables
    private Rigidbody playerRB;
    private Animator playerAnim;

    //game manager script to use game manager methods and variables
    private GameManager gm;

    //powerup objects
    public GameObject powerupIndicator;
    public GameObject rocket;

    //particles
    public ParticleSystem dust;
    public ParticleSystem explosion;

    //player audios
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;

    void Start()
    {
        //set components
        playerRB = GetComponent<Rigidbody>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        dust.Play();//play dust particles
    }


    void Update()
    {
        //if to far left stop moving
        if(transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        //if to far right stop moving
        else if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        //if player is on ground and running make animation running and get left/right input
        if(isOnGround && gm.isGameActive)
        {
            playerAnim.SetBool("Jump_b", false);
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
        //if on the ground and up arrow is pressed jump with animation and play jump sound, stop dust animation
        if(isOnGround && gm.isGameActive && Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerAnim.SetBool("Jump_b", true);
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dust.Stop();
        }
        //if the player has the gun powerup and presses space fire rocket
        if(Input.GetKeyDown(KeyCode.Space) && GunPower)
        {
            Instantiate(rocket, transform.position + new Vector3(0f, 1f, 1f), rocket.transform.rotation);
        }
        //set postion and rotation if power up indicator
        powerupIndicator.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
        powerupIndicator.transform.Rotate(0f, 0.5f, 0f);
    }


    private void OnCollisionEnter(Collision collision){  
        //if player hits strength power destroy the power up and activate the power
        if(collision.gameObject.CompareTag("StrPower")){
            Destroy(collision.gameObject);
            StrPowerMethod();
        }
        //if player hits gun power destroy the power up and activate the power
        else if(collision.gameObject.CompareTag("GunPower")){
            Destroy(collision.gameObject);
            GunPowerMethod();
        }
        //if player is on the ground set is on ground to true and play dust animation
        else if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
            dust.Play();
        }
        //if player hits an enemy...
        else if(collision.gameObject.CompareTag("Enemy")){
            //and player doesnt have strength power end game, show game over screen and play death animation and sound
            if(!StrPower){
                Debug.Log("GameOver");
                gm.GameOver();
                dust.Stop();
                explosion.Play();
                if(playerAnim.GetBool("Jump_b")){
                    playerAnim.SetInteger("DeathType_int", 2);
                }
                playerAnim.SetBool("Death_b", true);
                playerAudio.PlayOneShot(crashSound, 1.0f);
            }
            //and if player does have strength powerup destroy the enemy and increment
            else{
                Destroy(collision.gameObject);
                gm.UpdateScore(1);
            }
        }
    }
    //after power up is activated show powerup indicator and wait 5 seconds then deactivate
    IEnumerator PowerupCountdownRoutine()
    {
        powerupIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        powerupIndicator.gameObject.SetActive(false);
        playerAnim.SetFloat("Speed_f", 0.5f);
        gm.playerSpeed = 5;
        StrPower = false;
        GunPower = false;
    }
    //activate strength power, deactivate gun power, increase player speed(animation and objects speed) and set power up timer
    private void StrPowerMethod(){
        StrPower = true;
        GunPower = false;
        gm.playerSpeed = 10;
        playerAnim.SetFloat("Speed_f", 1f);
        StartCoroutine(PowerupCountdownRoutine());
    }
    //activate gun power, deactivate strength power and set power up timer
    private void GunPowerMethod(){
        StrPower = false;
        GunPower = true;
        StartCoroutine(PowerupCountdownRoutine());
    } 
}