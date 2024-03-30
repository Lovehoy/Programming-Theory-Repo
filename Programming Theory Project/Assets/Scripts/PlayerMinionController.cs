using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMinionController : Minions
{
    public float speed = 5f;
    public float jumpForce = 0.2f;

    private Vector3 direction;
    public Vector3 jump;

    public bool isGrounded;
    public bool gameOver;
    void Start()
    {
        jump = new Vector3(0.0f, .1f, 0.0f);
    }

    public void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        // get inputs
        direction.x = Input.GetAxis("Horizontal") * speed;
        direction.y = Input.GetAxis("Vertical") * speed;

        // free move
        if (!gameOver)
        {
            rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
        }
        
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameOver)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
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
