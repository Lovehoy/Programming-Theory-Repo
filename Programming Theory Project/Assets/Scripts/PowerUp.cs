using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
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
        // Check if the colliding object has the "Enemy" tag
        if (other.CompareTag("Player"))
        {
            // Destroy the gameobject
           Destroy(gameObject);
           Debug.Log("POWER UP destroyed");
        }

    }
}
