using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject barrelPrefab;
    public GameObject minionPrefab;
    public GameObject powerupPrefab;

    public int minionCount;

    public int waveNumber = 1;

    public float xSpawnRange = 5.0f;
    public float ySpawnRange = 10.0f;
    private float zSpawnRange = -10f;
    private float startBarrelDelay = 1;
    private float repeatBarrelRate = 5;

    private Vector3 barrelspawnPos = new Vector3(-2, 10, 0);

    // get reference to public playerControllerScript
    private PlayerMinionController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        SpawnMinionWave(waveNumber);
        InvokeRepeating("SpawnBarrel", startBarrelDelay, repeatBarrelRate);

        playerScript = GameObject.Find("Player").GetComponent<PlayerMinionController>();
    }

    // Update is called once per frame
    void Update()
    {
        minionCount = FindObjectsOfType<EnemyMinion>().Length;

        // when to spawn another enemy
        // if enemyCount hits zero
        if (minionCount == 1)
        {
            //then waveNumber increment by 1 (++)
            waveNumber++;
            //then spawn waveNumber of enemy
            SpawnMinionWave(waveNumber);
        }
    }

    void SpawnMinionWave(int minionsToSpawn)
    {
        // for loops control interations
        // for loop will run Instantiate method 3 times
        // 3 perameteres needed to use for loop : where to start (i = 0), stop condition (i < 3),
        // how to get i from 0 to 3 [(i = i+1) -> (i+=1) -> (i++)]
        minionsToSpawn = 3;

        for (int i = 0; i < minionsToSpawn; i++)
        {
            //Instantiate enemyPrefab using GenerateSpanPosition method
            Instantiate(minionPrefab, GenerateSpawnPosition(), minionPrefab.transform.rotation);
        }
    }
    
    // generate Vector3 data at randomPos
    //private return gernates data
    private Vector3 GenerateSpawnPosition()
    {
        // declare x and z spawnPos variables' range
        float spawnPosX = (Random.Range(-xSpawnRange, xSpawnRange));
        float spawnPosY = (Random.Range(1, ySpawnRange));
        // delcare randomPos
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, zSpawnRange);
        // get data from method
        return randomPos;
    }
    void SpawnBarrel()
    {
        // IF gameOver (public class from ControllerScript) is equal to false...
        // (== is equal to. = is equals)
        if (playerScript.gameOver == false)
        {
            //THEN Instantiate spawn obstaclePrefab at Vector3 spawnPos and orient obstacle rotation
            Instantiate(barrelPrefab, barrelspawnPos, barrelPrefab.transform.rotation);
        }
    }

}
