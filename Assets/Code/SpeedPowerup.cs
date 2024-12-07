using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float SpeedMultiplier = 2f; 
    public float PowerUpDuration = 5f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ActivateSpeedPowerUp(SpeedMultiplier, PowerUpDuration);
                Destroy(gameObject);
            }
        }
    }
}
