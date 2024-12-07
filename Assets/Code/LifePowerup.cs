using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class LifePowerup : MonoBehaviour
{
    private LivesManager livesManager;

    private void Start()
    {
        if (livesManager == null)
        {
            livesManager = FindObjectOfType<LivesManager>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                LivesManager.AddToLives(1);
                Destroy(gameObject);
            }
        }
    }
}
