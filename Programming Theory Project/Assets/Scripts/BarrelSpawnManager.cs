using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class BarrelSpawnManager : MonoBehaviour
{
    public GameObject barrelPrefab;
    public GameObject powerupPrefab;

    private float startBarrelDelay = 1;
    private float repeatBarrelRate = 5;

    private Barrel Barrel;

    //public Vector3 barrelspawnPos = new Vector3(-2, 10, 0);

    // Flag to indicate if the player prefab is instantiated
    private bool playerInstantiated = false;

    public event Action BarrelSpawned; // Define an event

    void Start()
    {
        // Delay the spawning of barrels until after the player prefab is instantiated
        Invoke("DelayedStart", 0.5f);
     
       
    }

    // Delayed start method to ensure player prefab is instantiated first
    void DelayedStart()
    {
        // Set playerInstantiated flag to true
        playerInstantiated = true;
        InvokeRepeating("SpawnBarrel", startBarrelDelay, repeatBarrelRate);
        // Subscribe to the OnBreak event of all barrels in the scene
        Barrel = Barrel.GetComponent<Barrel>();
        Barrel[] barrels = FindObjectsOfType<Barrel>();
        foreach (Barrel barrel in barrels)
        {
            {
                barrel.OnBreak.AddListener(SpawnPowerup);
            }
        }

        
    }

    void SpawnBarrel()
    {
        // If game over is false and player prefab is instantiated, spawn barrels
        if (playerInstantiated)
        {
            Instantiate(barrelPrefab, transform.position, barrelPrefab.transform.rotation);
            Rigidbody BarrelRb = Barrel.GetComponent<Rigidbody>();
            if (BarrelRb != null)
            {
                Vector3 pushDirection = Vector3.right;
                float pushForce = 1f;
                BarrelRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
            BarrelSpawned?.Invoke(); // Trigger the event
        }
    }
    public void SpawnPowerup(Vector3 position)
    {
        Instantiate(powerupPrefab, position, Quaternion.identity);
    }
}