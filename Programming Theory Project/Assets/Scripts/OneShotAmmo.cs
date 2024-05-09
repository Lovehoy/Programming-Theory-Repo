using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class OneShotAmmo : Ammo
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            // Check if the colliding object has the "Enemy" tag
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                Debug.Log("One Shot Hit!");
            }

            if (other.CompareTag("Barrel"))
            {
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Boss"))
            {
                Destroy(other.gameObject);
                Debug.Log("You Win! Boss Defeated!");
                gameManager.WinLevel();
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
