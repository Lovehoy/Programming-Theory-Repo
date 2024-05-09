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
   
    private float minFallDistance = .000000000001f; // Minimum fall distance to consider as a fall, must be very low to account for prefab scaling
    public LayerMask groundLayer; // Layer mask to specify which layer to consider as ground

    private Vector3 startPosition; // Initial position of the object
   
    private bool isFalling = false; // Flag to track if the object is falling
    public bool isBroken = false;
    private bool isGrounded = false; // Flag to track if the object is grounded

    private bool isburning = false;

    private Rigidbody rb; // Rigidbody component of the object

    public GameObject burnParticlePrefab; 
    private GameObject burnParticleInstance; 

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Record the initial position of the object
        startPosition = transform.position;

       // GameManager.Instance.OnOneShotAwardedChanged.AddListener(OnOneShotAwardedChanged);
    }

    void Update()
    {
        // Check if the object is grounded
        float groundDistance = .333f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);

        // If the object is not grounded and not already falling
        if (!isGrounded && !isFalling)
        {
            // Start tracking the fall
            isFalling = true;
            startPosition = transform.position;
           // Debug.Log(gameObject.name + "at" + startPosition);
        }

        // If the object is grounded and was falling
        else if (isGrounded && isFalling)
        {
            FindFallDistance();
        }

       // if (isburning)
      // {
       //    Burn();
       //    Debug.Log("Update passed to Burn()");
     // }
     // else if (!isburning && burnParticleInstance != null)
     //  {
            // Destroy shoot particle effect if it's currently instantiated
        //if (burnParticlePrefab != null)
        //   {
         //      Destroy(burnParticleInstance);
         //   }
        //    isburning = false;
      // }
    }

    public void FindFallDistance()
    {
        // Calculate the fall distance
        // float fallDistance = startPosition.y - transform.position.y;

        Vector3 displacement = transform.position - startPosition;
        float fallDistance = displacement.magnitude;

        // If the fall distance is greater than the minimum fall distance
        if (fallDistance > minFallDistance) //(rb.velocity.y < 1 && fallDistance > minFallDistance)
        {
           // Debug.Log(gameObject.name + " fell " + fallDistance + " units.");
            // GetComponent<Renderer>().material.color = breakColor;

            Break();
        }
    }
    public void Break()
    {
        Debug.Log("barrel Broken");
        rb.isKinematic = true;
        isFalling = false;
        BarrelSpawnManager barrelSpawnManager = FindObjectOfType<BarrelSpawnManager>();
          if (barrelSpawnManager != null)
                {
                    barrelSpawnManager.SpawnPowerup(transform.position);
                }
        Destroy(gameObject);
     }

   // public void Burn()
    //{
    //    Debug.Log("Burn()");
    //    isburning = true;
    //    if (burnParticleInstance == null)
     //   {
      //      burnParticleInstance = Instantiate(burnParticlePrefab, transform.position, Quaternion.identity);
      //      Debug.Log("burn playing");
      //      burnParticleInstance.transform.parent = transform;
      //  }
   // }

   // public bool StopBurning()
  //  {
      //  Debug.Log("stopburing()");
      //  return !isburning;
 //  }
    //private void OnOneShotAwardedChanged(bool isOneShotAwarded)
    //{
        // Perform actions based on the value of isOneShotAwarded
    //    if (isOneShotAwarded)
    //    {
   //         Burn();
     //       Debug.Log("Barrel got Oneshot");
    //    }
    //    else
    //    {
    //        StopBurning();
     //       Debug.Log("Barrel got !Oneshot");
   ///  }
   //}
    //private void OnDestroy()
    //{
      //  GameManager.Instance.OnOneShotAwardedChanged.RemoveListener(OnOneShotAwardedChanged);
  //  }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isburning)
        {
            Destroy(collision.gameObject);
            Break();
        }
    }
   

}