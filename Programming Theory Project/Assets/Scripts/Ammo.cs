using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour //4/10 Why aren't the hits hitting?
{
    public float speed = 10f;
    // Reference to the player GameObject
    public GameObject player;
    private PlayerController playerController;

    void Start()
    {
         player = GameObject.FindWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

      

    }

    // Update is called once per frame
    void Update()
    {


         Vector3 playerRight = player.transform.forward;
        Vector3 movementDirection = playerController.IsFacingRight() ? playerRight : -playerRight;
         transform.Translate(movementDirection * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // Destroy the gameobject
            Destroy(other.gameObject);
            Destroy(gameObject);
            Debug.Log("Enemy Hit!");
        }

        else
        { 
            //Destroy(gameObject);
            Debug.Log("miss");
        }

    }
}
