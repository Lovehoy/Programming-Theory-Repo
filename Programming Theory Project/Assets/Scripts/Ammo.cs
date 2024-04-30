using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ammo : MonoBehaviour 
{
 private float speed = 10f;

  float spawnOffsetDistance = -.5f;
    // Reference to the player GameObject
    public GameObject player;
    private PlayerController playerController;
   
    private Transform playerTransform;
    private Vector3 initialPosition;
    private Vector3 moveDirection;

    GameManager gameManager;

    public const int pointsMinion = 1;


   [SerializeField] private ParticleSystem hitParticlePrefab; // Reference to the particle effect prefab
    private ParticleSystem hitParticleInstance; // Reference to the particle effect prefab
    [SerializeField] private ParticleSystem missParticlePrefab; // Reference to the particle effect prefab
    private ParticleSystem missParticleInstance; // Reference to the particle effect prefab
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager instance in the scene
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();

        // Set the initial position of the projectile to be slightly offset from the player's position
        // Change the offset value as needed to place the projectile on the desired side of the player
        initialPosition = playerTransform.position + playerTransform.forward * spawnOffsetDistance;

        // Determine the initial movement direction based on the player's facing direction
        Vector3 playerForward = playerTransform.forward;
        moveDirection = playerController.IsFacingRight() ? -playerForward : playerForward;

        // Set the initial position of the projectile
        transform.position = initialPosition;

    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile along the predefined movement direction
        transform.Translate(moveDirection * speed * Time.deltaTime);
        DestroyOutOfBounds();
}

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player"))
        {
            // Check if the colliding object has the "Enemy" tag
            if (other.CompareTag("Enemy"))
            {
                // Destroy the gameobject
                Destroy(other.gameObject);
                Destroy(gameObject);
                Debug.Log("Enemy Hit!");
                hitParticleInstance = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
                gameManager.AwardPoints(pointsMinion);
            }

            else
            {
                Destroy(gameObject);
                Debug.Log("Miss");
                missParticleInstance = Instantiate(missParticlePrefab, transform.position, Quaternion.identity);
            }
        }

    }

    void DestroyOutOfBounds ()
    {
        float rightBound = 14;
        float fwBound = 7f;
        if (transform.position.x > rightBound)
        {
           // Debug.Log("rightBound");
            Destroy(gameObject);
        }
        else if (transform.position.x < -rightBound)
        {
           // Debug.Log("leftBound");
            Destroy(gameObject);
        }
        else if (transform.position.z > fwBound)
        {
            //Debug.Log("fwBound");
            Destroy(gameObject);
        }
    }
}
