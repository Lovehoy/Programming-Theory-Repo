using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

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
       Move();
      Shoot();
        //if space, then force pulse all other Rb away
    }

    private void Move()
    {
        //get inputs
       direction.x = Input.GetAxis("Horizontal") * speed;
      direction.y = Input.GetAxis("Vertical") * speed;


        // free move
       if (Input.GetKeyDown(KeyCode.UpArrow) && !gameOver && isGrounded)
        {
          rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
           

      }
        if (!gameOver && isGrounded)
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
                pooledProjectile.transform.position = transform.position; // position it at player
            }
        }
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
        }
    }
}
