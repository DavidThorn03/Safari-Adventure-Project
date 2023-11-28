using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player controller script is on the player object
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.EnableDestroyAbility();
                Destroy(gameObject); // Remove the power-up from the scene
            }
        }
    }
}



