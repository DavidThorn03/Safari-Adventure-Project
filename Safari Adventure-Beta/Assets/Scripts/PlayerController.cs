using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float speed = 10.0f;
    private float xRange = 9;
    private float jumpForce = 600;

    private bool StrPower = false;
    private bool GunPower = false; 
    private bool isOnGround = false;

    private Rigidbody playerRB;
    private GameManager gm;
    private Animator playerAnim;
    
    public GameObject powerupIndicator;
    public GameObject rocket;

    public ParticleSystem dust;
    public ParticleSystem explosion;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        dust.Play();
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
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dust.Stop();
        }
        if(Input.GetKeyDown(KeyCode.Space) && GunPower){
            Instantiate(rocket, transform.position + new Vector3(0f, 1f, 1f), rocket.transform.rotation);
        }

        powerupIndicator.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
        powerupIndicator.transform.Rotate(0f, 0.5f, 0f);
    }


    private void OnCollisionEnter(Collision collision){  
        if(collision.gameObject.CompareTag("StrPower")){
            Destroy(collision.gameObject);
            StrPowerMethod();
        }

        else if(collision.gameObject.CompareTag("GunPower")){
            Destroy(collision.gameObject);
            GunPowerMethod();
        }

        else if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
            dust.Play();
        }

        else if(collision.gameObject.CompareTag("Enemy")){
            if(!StrPower){
                Debug.Log("GameOver");
                gm.GameOver();
                explosion.Play();
                if(playerAnim.GetBool("Jump_b")){
                    playerAnim.SetInteger("DeathType_int", 2);
                }
                playerAnim.SetBool("Death_b", true);
                playerAudio.PlayOneShot(crashSound, 1.0f);
            }
            else{
                Destroy(collision.gameObject);
                gm.UpdateScore(1);
            }
            dust.Stop();
        }
    }

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
    private void StrPowerMethod(){
        StrPower = true;
        GunPower = false;
        gm.playerSpeed = 10;
        playerAnim.SetFloat("Speed_f", 1f);
        StartCoroutine(PowerupCountdownRoutine());
    }
    private void GunPowerMethod(){
        StrPower = false;
        GunPower = true;
        StartCoroutine(PowerupCountdownRoutine());
    } 
}