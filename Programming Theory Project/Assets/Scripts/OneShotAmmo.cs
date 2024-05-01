using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAmmo : Ammo
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            // Check if the colliding object has the "Enemy" tag
            if (other.CompareTag("Enemy"))
            {
                // Destroy the gameobject
                //gameManager.AwardPoints(pointsMinion);
                Destroy(other.gameObject);
                Debug.Log("One Shot Hit!");
            }

            if (other.CompareTag("Barrel"))
            {
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Boss"))
            {
                // Destroy the gameobject
                //gameManager.AwardPoints(pointsMinion);
                Destroy(other.gameObject);
                Debug.Log("You Win! Boss Defeated!");
            }

            else if (other.CompareTag("DeathZone"))
            {
                
                //Destroy(gameObject);
                Debug.Log("miss");
                Destroy(gameObject);
            }
        }

    }
}
