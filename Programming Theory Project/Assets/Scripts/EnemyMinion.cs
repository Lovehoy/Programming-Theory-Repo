using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMinion : Minions
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        minionRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }
    public override void FixedUpdate()
    {
        // look twoards player direction
         Vector3 lookDirection = (player.transform.position - transform.position);
         minionRb.MovePosition(lookDirection * Time.deltaTime);

        // === enemyRb FOLLOWS player GameObject ======
        // AddForce to enemyRb with lookDirection times the enemy's speed
        minionRb.AddForce(lookDirection * speed);

        // Destroy
        Destroy();

    }
}
