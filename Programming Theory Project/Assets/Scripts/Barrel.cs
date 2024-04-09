using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour

{
    private float minFallDistance = 5f; // Minimum fall distance to consider as a fall
    public LayerMask groundLayer; // Layer mask to specify which layer to consider as ground

    private Vector3 startPosition; // Initial position of the object
    private bool isFalling = false; // Flag to track if the object is falling
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, .15f, groundLayer);

        // If the object is not grounded and not already falling
        if (!isGrounded && !isFalling)
        {
            // Start tracking the fall
            isFalling = true;
            startPosition = transform.position;
        }

        // If the object is grounded and was falling
        if (isGrounded && isFalling)
        {
            Break();
        }
    }

    void Break()
    {
        // Calculate the fall distance
        float fallDistance = startPosition.y - transform.position.y;

        // If the fall distance is greater than the minimum fall distance
        if (fallDistance > minFallDistance) //(rb.velocity.y < 1 && fallDistance > minFallDistance)
        {
            Debug.Log(gameObject.name + " fell " + fallDistance + " units.");
            GetComponent<Renderer>().material.color = breakColor;

            rb.isKinematic = true;
            // Deactivate the Rigidbody component
            //Destroy(rb.gameObject);

            // Deactivate the MeshRenderer component
            isFalling = false;
        }
    }
}