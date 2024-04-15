using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject camPrefab; // Reference to the player prefab

    void Start()
    {
        // Instantiate the player prefab at the position of the PlayerSpawner GameObject
        Instantiate(camPrefab, transform.position, Quaternion.identity);
    }
}
