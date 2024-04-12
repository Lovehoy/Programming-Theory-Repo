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
    public bool gameOver;

    private Vector3 direction;
    public Vector3 jump;

    public Rigidbody rb;

    //public GameObject projectilePrefab;
    void Start()
    {
        jump = new Vector3(0.0f, .5f, 0.0f);
    }

    public void FixedUpdate()
    {
        if (!gameOver)
        {
            Move();
            Shoot();
        }
        
        //if space, then force pulse all other Rb away
    }

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
   //make a pooling method for ammo
   private void Shoot()
   {
        if (Input.GetKey(KeyCode.Space))
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
            }
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

    //compares collision w/ Tags
    private void OnCollisionEnter(Collision collision)
    {
        // if Player collides with Enemey, then "Game Over!"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameOver = true;

            Debug.Log("Game Over!");
            rb.mass = 0f;

        }
    }
}
