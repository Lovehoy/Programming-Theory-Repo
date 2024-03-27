using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject minionPrefab;
    public GameObject powerupPrefab;

    public int minionCount;

    public int waveNumber = 1;

    private float spawnRange = 9.0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnMinionWave(waveNumber);

    }

    // Update is called once per frame
    void Update()
    {
        minionCount = FindObjectsOfType<EnemyMinion>().Length;

        // when to spawn another enemy
        // if enemyCount hits zero
        if (minionCount == 0)
        {
            //then waveNumber increment by 1 (++)
            waveNumber++;
            //then spawn waveNumber of enemy
            SpawnMinionWave(waveNumber);

            //then instantiate another powerupPrefab
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    // generate Vector3 data at randomPos
    //private return gernates data
    private Vector3 GenerateSpawnPosition()
    {
        // declare x and z spawnPos variables' range
        float spawnPosX = (Random.Range(-spawnRange, spawnRange));
        float spawnPosY = (Random.Range(-spawnRange, spawnRange));
        // delcare randomPos
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, 100);
        // get data from method
        return randomPos;
    }

    void SpawnMinionWave(int minionsToSpawn)
    {
        // for loops control interations
        // for loop will run Instantiate method 3 times
        // 3 perameteres needed to use for loop : where to start (i = 0), stop condition (i < 3),
        // how to get i from 0 to 3 [(i = i+1) -> (i+=1) -> (i++)]
        for (int i = 0; i < minionsToSpawn; i++)
        {
            //Instantiate enemyPrefab using GenerateSpanPosition method
            Vector3 spawnPos = Vector3.zero;
            Instantiate(minionPrefab, spawnPos, minionPrefab.transform.rotation);
        }
    }

}
