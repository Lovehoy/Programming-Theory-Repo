using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ammo : MonoBehaviour //4/10 Why aren't the hits hitting?
{
    public float speed = 10f;
    // Reference to the player GameObject
    public GameObject player;
    private PlayerController playerController;
   
    private Transform playerTransform;
    private Vector3 initialPosition;
    private Vector3 moveDirection;

    void Start()
    {
        //  player = GameObject.FindWithTag("Player");
        //  playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Find the player GameObject by tag
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();

        // Set the initial position of the projectile to be slightly offset from the player's position
        // Change the offset value as needed to place the projectile on the desired side of the player
        initialPosition = playerTransform.position + playerTransform.forward * 1.5f;

        // Determine the initial movement direction based on the player's facing direction
        Vector3 playerForward = playerTransform.forward;
        moveDirection = playerController.IsFacingRight() ? playerForward : -playerForward;

        // Set the initial position of the projectile
        transform.position = initialPosition;

    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile along the predefined movement direction
        transform.Translate(moveDirection * speed * Time.deltaTime);
        DestroyOutOfBounds();
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
            Destroy(gameObject);
        }

    }

    void DestroyOutOfBounds ()
    {
        float rightBound = 14;
        float fwBound = 7f;
        if (transform.position.x > rightBound)
        {
            Debug.Log("rightBound");
            Destroy(gameObject);
        }
        else if (transform.position.x < -rightBound)
        {
            Debug.Log("leftBound");
            Destroy(gameObject);
        }
        else if (transform.position.z > fwBound)
        {
            Debug.Log("fwBound");
            Destroy(gameObject);
        }
    }
}
