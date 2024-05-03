using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    //   float spawnOffsetDistance = 1f;

    public bool isGrounded;
    public bool gameOver = false;
    public bool canShoot = false; // Variable to track if player can shoot
    private bool spacePressed = false;
    public bool oneShotAwarded = false;

    private Vector3 direction;
    public Vector3 jump;
    private Vector3 moveDirection;

    public Rigidbody rb;

    public GameObject oneShotPrefab;

    public Color originalColor;
    public Color PUpColor;
    public Color LowAmmoColor;
    public Color OneShotColor;

    private int maxProjectiles = 5; // Maximum number of projectiles allowed per power-up
    private int lowProjectiles = 3; // Number of projectiles fired to trigger getting low
    private int projectilesFired = 0; // Number of projectiles fired
                                      // public int maxPoints = 10;
                                      //   public int minPoints = 0;
                                      //  public int currentPoints;
    public GameManager GameManager;

    [SerializeField] private Animator animator = null;

    private List<GameObject> activeParticles = new List<GameObject>(); // Store references to active particle systems

    public GameObject shootParticlePrefab; // Reference to the shoot particle effect prefab

    private GameObject shootParticleInstance; // Instance of the shoot particle effect
    void Start()
    {
        animator = GetComponent<Animator>();
        //jump = new Vector3(0.0f, .5f, 0.0f);
        GameManager = FindObjectOfType<GameManager>(); // Find the GameManager instance in the scene
        //currentPoints = minPoints;
    }

    public void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                spacePressed = false;
            }
            // Check if the player can shoot based on available ammo
            if (canShoot && CanShoot())
            {
                // Call your shooting method
                Shoot();
               // PlayCanShootVFX();
               
            }
            else if (oneShotAwarded)
            {
                ShootOneShot();
            }
            //   if (Input.GetKey(KeyCode.RightAlt))
            //{
            //    ShootOneShot();
            // }
            else
            {
                // Destroy shoot particle effect if it's currently instantiated
                if (shootParticlePrefab != null)
                {
                    Destroy(shootParticleInstance);
                }
                canShoot = false;

            }
        }


    }
    private void FixedUpdate()
    {
        if (!gameOver)
            Move();
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
        // 180 rule film bros
        //may need to be changed back. we need a character to see
        if (Mathf.Abs(horizontalInput) > 0.1f) // Check if there's significant horizontal input
        {
            // Calculate the angle in radians between the current forward direction and the movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            // Ensure the target angle is always a full 180 degrees
            targetAngle += 180f;
            // Create a rotation quaternion based on the target angle around the y-axis
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Apply the rotation directly to the player's transform
            // transform.rotation = targetRotation;

            // Create a rotation quaternion based on the target angle around the y-axis
            // Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

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

        moveDirection = new Vector3(direction.x, 0, direction.y);


        if (moveDirection == Vector3.right)
        {
            animator.SetBool("Right", true);
        }

        else if (moveDirection == Vector3.left)
        {
            animator.SetBool("Left", true);
        }

        else if (moveDirection == Vector3.up)
        {
            animator.SetBool("Jump", true);
        }

        else if (moveDirection == Vector3.down)
        {
            animator.SetBool("Fall", true);
        }

        else
        {
            //Idle
            animator.SetBool("Idle", true);
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

            animator.SetTrigger("Shoot");
            spacePressed = true;
            // Get an object object from the pool
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                projectilesFired++;
                //isShooting = false;
            }
            // else
            // {
            //     animator.ResetTrigger("Shoot");
            // }

        }

    }

    public void ShootOneShot()
    {
        oneShotAwarded = true;
        if (Input.GetKey(KeyCode.RightAlt))
        {
            animator.SetTrigger("Shoot");
            Debug.Log("ShootOneShot() pressed");

            Debug.Log("ONE SHOT SHOT");
            oneShotAwarded = false;
        }
    }
    private bool CanShoot()
    {
        // Check if the number of projectiles fired is less than the maximum allowed
        return projectilesFired < maxProjectiles;
        
    }
    public void EnableShooting()
    {
        // Enable shooting ability
        Debug.Log("Can shoot");
        canShoot = true;
        projectilesFired = 0;
        PlayCanShootVFX();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            // canShoot = true; // Enable shooting ability
            Destroy(other.gameObject);
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
            animator.SetBool("Die", true);
            Debug.Log("Game Over!");
            rb.mass = 0f;
            GameManager.GameOver();
        }
    }

    // ************** PARTICLES *****************
    void PlayCanShootVFX()
    {
        if (shootParticleInstance == null)
        {
            shootParticleInstance = Instantiate(shootParticlePrefab, transform.position, Quaternion.identity);
            Debug.Log("VFX playing");
            shootParticleInstance.transform.parent = transform;
        }

        if (projectilesFired <= lowProjectiles)
        {
            //////////////////////////////////
        }
    }
    private void DeactivateActiveParticles()
    {
        foreach (GameObject particle in activeParticles)
        {
            particle.SetActive(false);
        }
        activeParticles.Clear(); // Clear the list
    }
}
