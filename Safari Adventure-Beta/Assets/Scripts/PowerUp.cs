using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    public bool StrPower = false;
    public bool GunPower = false; 
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(CompareTag("StrPower")){
                //strength powerup
                StrPower = true;
            }
            if(CompareTag("GunPower")){
                //gunPowerUp
                GunPower = true;
            }
        }
    }
}



