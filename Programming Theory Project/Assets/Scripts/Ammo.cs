using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour //4/10 Why aren't the hits hitting?
{
    public float speed = 10f;
    // Reference to the player GameObject
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindWithTag("Player");
        // Ensure the player reference is set
        if (player == null)
        {
            Debug.LogError("Player reference not set in Ammo script!");
        }
        else
        {
            // Instantiate the projectile in the forward direction of the player
            transform.position = player.transform.position ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in its forward direction
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the gameobject
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Debug.Log("Enemy Hit!");
        }

        else
        { 
            Destroy(gameObject);
            Debug.Log("miss");
        }

    }
}
