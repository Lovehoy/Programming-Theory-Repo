using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab

    void Start()
    {
        // Instantiate the player prefab at the position of the PlayerSpawner GameObject
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}
