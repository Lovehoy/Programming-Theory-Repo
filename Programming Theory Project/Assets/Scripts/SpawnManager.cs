using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public GameObject minionPrefab;

    public int minionCount;

    public int waveNumber = 1;

    // Flag to indicate if the player prefab is instantiated
    private bool playerInstantiated = false;

    //public event Action BarrelSpawned; // Define an event

    private List<Transform> activeMinions = new List<Transform>();

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

        // Set playerInstantiated flag to true
        playerInstantiated = true;
    }

    // Update is called once per frame
    void Update()
    {
      
    // Check if the player prefab is instantiated before updating minion count and spawning minions
    if (playerInstantiated)
    {
        minionCount = FindObjectsOfType<EnemyMinion>().Length;

        // When to spawn another wave of minions
        if (minionCount <= 1)
        {
            // Spawn the next wave of minions
            SpawnMinionWave(waveNumber);
        }
    }
    }

    // ***************** Minions not respawning *********************************
    void SpawnMinionWave(int minionsToSpawn)
    {
        //Debug.Log("Spawning " + minionsToSpawn + " minions.");

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
        float spawnRange = .5f;

        // Generate random spawn position within the defined range
        float spawnPosX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = UnityEngine.Random.Range(-spawnRange, spawnRange);

        // Ensure that the spawn position is above ground level (adjust as needed)
        float spawnPosY = UnityEngine.Random.Range(-spawnRange, spawnRange);
     
        return new Vector3(spawnPosX, spawnPosY, spawnPosZ);
    }

}