using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAmmo : Ammo
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
                Destroy(gameObject);
                Debug.Log("Enemy Hit!");

            }

            else
            {
                //Destroy(gameObject);
                Debug.Log("miss");
                Destroy(gameObject);
            }
        }

    }
}
