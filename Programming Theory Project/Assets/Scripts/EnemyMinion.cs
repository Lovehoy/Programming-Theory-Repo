using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMinion : Minions
{
    public float speed = 1f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }
    public  void FixedUpdate()
    {
        // look twoards player direction
        Vector3 lookDirection = (player.transform.position - transform.position).normalized ;

        // === enemyRb FOLLOWS player GameObject ======
        // AddForce to enemyRb with lookDirection times the enemy's speed
        rb.AddForce(lookDirection * speed);

    }
}
