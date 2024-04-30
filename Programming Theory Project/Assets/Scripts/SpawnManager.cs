using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
  //  public GameObject barrelPrefab;
    public GameObject minionPrefab;
  //  public GameObject powerupPrefab;

    public int minionCount;

    public int waveNumber = 1;

   // private float startBarrelDelay = 1;
  //  private float repeatBarrelRate = 5;

   // private Barrel barrelController;

  //  public Vector3 barrelspawnPos = new Vector3(-2, 10, 0);

    // Flag to indicate if the player prefab is instantiated
    private bool playerInstantiated = false;

    //public event Action BarrelSpawned; // Define an event


    private List<Transform> activeMinions = new List<Transform>();
    //private int minion;


    // Start is called before the first frame update
    void Start()
    {
        // Delay the spawning of minions and barrels until after the player prefab is instantiated
        Invoke("DelayedStart", 0.5f);
        // Start spawning barrels
       
    }

    // Delayed start method to ensure player prefab is instantiated first
    void DelayedStart()
    {
        // Spawn the initial wave of minions
        Debug.Log("Start SpawnMinionWave");
        SpawnMinionWave(waveNumber);

      //  InvokeRepeating("SpawnBarrel", startBarrelDelay, repeatBarrelRate);
        // Subscribe to the OnBreak event of all barrels in the scene
       // barrelController = barrelController.GetComponent<Barrel>();
       // Barrel[] barrels = FindObjectsOfType<Barrel>();
       // foreach (Barrel barrel in barrels)
      //  {
         //   {
            //    barrel.OnBreak.AddListener(SpawnPowerup);
      //     // }
       // }

        // Set playerInstantiated flag to true
        playerInstantiated = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player prefab is instantiated before updating minion count and spawning minions
        if (playerInstantiated)
        {
            minionCount = activeMinions.Count;

            // When to spawn another wave of minions
            if (minionCount <= 1)
            {
                // Increment wave number
                waveNumber++;
                // Spawn the next wave of minions
                SpawnMinionWave(waveNumber);
            }
        }
    }

    void SpawnMinionWave(int minionsToSpawn)
    {
        Debug.Log("Spawning " + minionsToSpawn + " minions.");

        for (int i = 0; i < minionsToSpawn; i++)
        {
            // Instantiate minionPrefab using GenerateSpawnPosition method
            Vector3 spawnPosition = RandomizeSpawnPosition();
            GameObject newMinionObject = Instantiate(minionPrefab, spawnPosition + transform.position, Quaternion.identity);
            Transform newMinionTransform = newMinionObject.transform;
            activeMinions.Add(newMinionTransform);
        }
    }
    public void MinionDestroyed(Transform minion)
    {
        // Remove the destroyed minion from the list of active minions
        activeMinions.Remove(minion);
    }
    Vector3 RandomizeSpawnPosition()
    {
        // Define the range within which minions can spawn
        float spawnRange = 3;

        // Generate random spawn position within the defined range
        float spawnPosX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = UnityEngine.Random.Range(-spawnRange, spawnRange);

        // Ensure that the spawn position is above ground level (adjust as needed)
        float spawnPosY = UnityEngine.Random.Range(-spawnRange, spawnRange);
     
        return new Vector3(spawnPosX, spawnPosY, spawnPosZ);
    }

   // void SpawnBarrel()
  //  {
        // If game over is false and player prefab is instantiated, spawn barrels
    //    if (playerInstantiated)
     //   {
      //      Instantiate(barrelPrefab, barrelspawnPos, barrelPrefab.transform.rotation);
        //    BarrelSpawned?.Invoke(); // Trigger the event
      //  }
    //}

   
}