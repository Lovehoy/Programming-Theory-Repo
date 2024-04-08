using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMinionController : Minions
{
    public float speed = 5f;
    public float jumpForce = 10f;

    public bool isGrounded;
    public bool gameOver;

    private Vector3 direction;
    public Vector3 jump;

    //public GameObject projectilePrefab;
    void Start()
    {
        jump = new Vector3(0.0f, .5f, 0.0f);
    }

    public void FixedUpdate()
    {
       Move();
        //if space, then force pulse all other Rb away
      //  Shoot();
     
    }

    //private void OldMove()
   // {
        // get inputs
     ///   direction.x = Input.GetAxis("Horizontal") * speed;
      //  direction.y = Input.GetAxis("Vertical") * speed;

        // free move
       // if (!gameOver)
       // {
        //    rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
       // }
        
 //   }
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
   // private void Shoot()
   // {
     //   if (Input.GetKeyDown(KeyCode.Space))
     //   {
      //      Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
       // }
   // }
 
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
