using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public GameObject barrelPrefab;
    public GameObject minionPrefab;
    public GameObject powerupPrefab;

    public int minionCount;

    public int waveNumber = 1;

    public float xSpawnRange = 5.0f;
    public float ySpawnRange = 10f;
    private float zSpawnRange = -10f;
    private float startBarrelDelay = 1;
    private float repeatBarrelRate = 5;

    private Barrel barrelController;

    public Vector3 barrelspawnPos = new Vector3(-2, 10, 0);

    // Flag to indicate if the player prefab is instantiated
    private bool playerInstantiated = false;

    // Start is called before the first frame update
    void Start()
    {
        // Delay the spawning of minions and barrels until after the player prefab is instantiated
        Invoke("DelayedStart", 0.5f);
        barrelController = barrelController.GetComponent<Barrel>();

        // Subscribe to the OnBreak event of all barrels in the scene
        Barrel[] barrels = FindObjectsOfType<Barrel>();
        foreach (Barrel barrel in barrels)
        {
            {
                barrel.OnBreak.AddListener(SpawnPowerup);
            }
        }
    }

    // Delayed start method to ensure player prefab is instantiated first
    void DelayedStart()
    {
        // Spawn the initial wave of minions
        SpawnMinionWave(waveNumber);

        // Start spawning barrels
        InvokeRepeating("SpawnBarrel", startBarrelDelay, repeatBarrelRate);

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
            if (minionCount == 1)
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
        for (int i = 0; i < minionsToSpawn; i++)
        {
            // Instantiate minionPrefab using GenerateSpawnPosition method
            Instantiate(minionPrefab, GenerateSpawnPosition(), minionPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        // Generate random spawn position within specified range
        float spawnPosX = Random.Range(-xSpawnRange, xSpawnRange);
        float spawnPosY = Random.Range(1, ySpawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, zSpawnRange);
        return randomPos;
    }

    void SpawnBarrel()
    {
        // If game over is false and player prefab is instantiated, spawn barrels
        if (playerInstantiated)
        {
            Instantiate(barrelPrefab, barrelspawnPos, barrelPrefab.transform.rotation);
        }
    }

    public void SpawnPowerup(Vector3 position)
    {
        Instantiate(powerupPrefab, position, Quaternion.identity);
    }
}