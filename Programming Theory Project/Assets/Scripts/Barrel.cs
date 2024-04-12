using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Barrel : MonoBehaviour

{
    public UnityEvent<Vector3> OnBreak;
    // public event Action<Vector3> OnBarrelBreak;

    private float minFallDistance = 5f; // Minimum fall distance to consider as a fall
    public LayerMask groundLayer; // Layer mask to specify which layer to consider as ground

    private Vector3 startPosition; // Initial position of the object
   
    private bool isFalling = false; // Flag to track if the object is falling
    public bool isBroken = false;
    private bool isGrounded = false; // Flag to track if the object is grounded

    private Rigidbody rb; // Rigidbody component of the object



    public Color breakColor;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Record the initial position of the object
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Check if the object is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, .148f, groundLayer);

        // If the object is not grounded and not already falling
        if (!isGrounded && !isFalling)
        {
            // Start tracking the fall
            isFalling = true;
            startPosition = transform.position;
            Debug.Log(gameObject.name + "at" + startPosition);
        }

        // If the object is grounded and was falling
        if (isGrounded && isFalling)
        {
            Break();
        }
    }

    public void Break()
    {
        // Calculate the fall distance
        float fallDistance = startPosition.y - transform.position.y;

        // If the fall distance is greater than the minimum fall distance
        if (fallDistance > minFallDistance) //(rb.velocity.y < 1 && fallDistance > minFallDistance)
        {
            //Debug.Log(gameObject.name + " fell " + fallDistance + " units.");
            GetComponent<Renderer>().material.color = breakColor;

            rb.isKinematic = true;
            isFalling = false;

            isBroken = true;

            OnBreak.Invoke(transform.position);

            if (isBroken)
            {
                SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
                if (spawnManager != null)
                {
                    spawnManager.SpawnPowerup(transform.position);
                }
                //OnBarrelBreak?.Invoke(transform.position);

                //  InstantiatePowerup(transform.position);
                // Deactivate the MeshRenderer component
                // Deactivate the Rigidbody component
                //Destroy(rb.gameObject);
            }


        }
    }
   // private void InstantiatePowerup(Vector3 position)
   // {
        // Instantiate the powerup prefab at the specified position
       // Instantiate(powerupPrefab, position, Quaternion.identity);
  //  }
}