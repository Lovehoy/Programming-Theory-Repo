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
    private bool shootPressed = false;
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

    private int maxProjectiles = 10; // Maximum number of projectiles allowed per power-up
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

    public AudioClip[] shootClips; // Array to hold the audio clips
    public AudioClip OneShotClip; 
    public AudioClip DieClip;
    private AudioSource audioSource; // Reference to the AudioSource component
    void Start()
    {
        animator = GetComponent<Animator>();
        //jump = new Vector3(0.0f, .5f, 0.0f);
        GameManager = FindObjectOfType<GameManager>(); // Find the GameManager instance in the scene
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    public void Update()
    {
        if (!gameOver)
        {
           if (Input.GetKeyUp(KeyCode.Space))
            {
                shootPressed = false;
            }
            // Check if the player can shoot based on available ammo
            if (canShoot && CanShoot())
            {
                // Call your shooting method
                Shoot();
               // PlayCanShootVFX();
               
            }
          
            else
            {
                // Destroy shoot particle effect if it's currently instantiated
                if (shootParticlePrefab != null)
                {
                    Destroy(shootParticleInstance);
                }
                canShoot = false;

            }

            if (Input.GetKeyDown(KeyCode.RightAlt))
            {
                ShootOneShot();
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


       
        if (Mathf.Abs(horizontalInput) > 0.1f) // Check if there's significant horizontal input
        {
            //float lta
            // Calculate the angle in radians between the current forward direction and the movement direction
            float targetAngle = direction.x > 0 ? 90 : 270;

            // Ensure the target angle is always a full 180 degrees
            targetAngle += 180f;
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
        if (Input.GetKey(KeyCode.Space) && !shootPressed)
        {
            Debug.Log("Shoot");

            animator.SetTrigger("Shoot");
            shootPressed = true;
            // Get an object object from the pool
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                projectilesFired++;
                PlayShootClip();
            }
            // else
            // {
            //     animator.ResetTrigger("Shoot");
            // }

        }

    }
    private void PlayShootClip()
    {
        if (shootClips.Length > 0)
        {
            // Pick a random clip from the array
            AudioClip clipToPlay = shootClips[Random.Range(0, shootClips.Length)];
            audioSource.PlayOneShot(clipToPlay);
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
// ************** ONE SHOT *****************
public void ShootOneShot()
{
        Debug.Log("F pressed");
        if (oneShotAwarded)
           {
        Instantiate(oneShotPrefab);
            //animator.SetTrigger("Shoot");
            audioSource.PlayOneShot(OneShotClip);
            Debug.Log("ONE SHOT SHOT");
            oneShotAwarded = false;
            GameManager.ResetPoints();


       }
}
    public bool HasAward()
    {
        Debug.Log("playerController receieved oneShotRewarded");
        //ShootOneShot();
        return oneShotAwarded = true;
       
    }
    // ************** COLLISION *****************
    private void OnCollisionEnter(Collision collision)
    {
        // if Player collides with Enemy, then "Game Over!"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Minion kill");
            Die();
        }
       
        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Boss Killed Player");
            Die();

        }

        /// ############ Problems with Barrel instantiation. Possibly return to later
       // if (oneShotAwarded && collision.gameObject.CompareTag("Barrel"))
       // {
        //    Debug.Log("Barrel kill");
        //    Die();
      //  }

    }

    public void Die()
    {
        if (!gameOver)
        {
            gameOver = true;
            animator.SetBool("Die", true);
            Debug.Log("Died");
            audioSource.PlayOneShot(DieClip);
            rb.mass = 0f;
            float deathFloatForce = 1f;
            rb.AddForce(Vector3.up * deathFloatForce, ForceMode.Acceleration);
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
