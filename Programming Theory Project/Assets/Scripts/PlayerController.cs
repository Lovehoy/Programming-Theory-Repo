using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    float spawnOffsetDistance = .222f;

    public bool isGrounded;
   public bool gameOver = false;
    public bool canShoot = false; // Variable to track if player can shoot
    private bool spacePressed = false;

    private Vector3 direction;
    public Vector3 jump;

    public Rigidbody rb;

    public Color originalColor;
    public Color PUpColor;
    public Color LowAmmoColor;

    private int maxProjectiles = 50; // Maximum number of projectiles allowed per power-up
    private int lowProjectiles = 40; // Number of projectiles fired to trigger getting low
    private int projectilesFired = 0; // Number of projectiles fired

    public GameManager GameManager;



    //public GameObject projectilePrefab;
    void Start()
    {
        jump = new Vector3(0.0f, .5f, 0.0f);
        GameManager = FindObjectOfType<GameManager>(); // Find the GameManager instance in the scene
    }

    public void FixedUpdate()
    {
        if (!gameOver)
        {
            Move();
            if (Input.GetKeyUp(KeyCode.Space))
            {
                spacePressed = false;
            }
            // Check if the player can shoot based on available ammo
            if (canShoot && CanShoot())
            {
                // Call your shooting method
                Shoot();
                // Change color if shooting
                GetComponent<Renderer>().material.color = PUpColor;
                Debug.Log("SHOOT NOW");
                if (projectilesFired <= lowProjectiles)
                {
                    GetComponent<Renderer>().material.color = LowAmmoColor;
                    Debug.Log("LOW ammo");
                }
            }
            
            else
            {
                // Optionally, provide feedback to the player
                Debug.Log("out of ammo");

                // Revert to original color
                GetComponent<Renderer>().material.color = originalColor;

                // Disable shooting ability if out of ammo
                canShoot = false;
            }
        }
        //if space, then force pulse all other Rb away
    }
    // ************** MOVEMENT *****************
    private void Move()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        float rotationSpeed = 20f;

        //get inputs
        direction.x = Input.GetAxis("Horizontal") * speed;
      direction.y = Input.GetAxis("Vertical") * speed;


        // Rotate the player character to face the direction it's moving on the x-axis
        if (Mathf.Abs(horizontalInput) > 0.1f) // Check if there's significant horizontal input
        {
            // Calculate the angle in radians between the current forward direction and the movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            // Create a rotation quaternion based on the target angle around the y-axis
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Apply the rotation to the player's transform
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (isGrounded)
        {
           rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);

        }

    }
    public bool IsFacingRight()
    {
        // Get the player's forward direction
        Vector3 forward = transform.right;

        // Check if the forward direction is pointing towards the positive x-axis (right direction)
        return forward.y >= 0f;
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // ************** SHOOTING *****************
 
        private void Shoot()
        {
            if (Input.GetKey(KeyCode.Space) && !spacePressed)
            {
                // Get an object object from the pool
                GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
                if (pooledProjectile != null)
                {
                    pooledProjectile.SetActive(true); // activate it
                    Vector3 spawnPosition = transform.position + transform.forward * spawnOffsetDistance;
                    pooledProjectile.transform.position = spawnPosition;
                    //Optionally, also set the rotation to match the player's rotation
                    //pooledProjectile.transform.rotation = transform.rotation;
                    // Increment projectiles fired
                    projectilesFired++;
                }
            }
        }
    private bool CanShoot()
    {
        // Check if the number of projectiles fired is less than the maximum allowed
        return projectilesFired < maxProjectiles;
    }
    private void EnableShooting()
    {
        // Enable shooting ability
        canShoot = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            // canShoot = true; // Enable shooting ability
            projectilesFired = 0; // Reset projectiles fired
            Destroy(other.gameObject);
            Debug.Log("POWER UP!");
            EnableShooting();// Call function to enable shooting ability

        }
    }
    // ************** COLLISION *****************
    private void OnCollisionEnter(Collision collision)
    {
        // if Player collides with Enemy, then "Game Over!"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            rb.mass = 0f;
            GameManager.GameOver();
        }
    }
    
}
