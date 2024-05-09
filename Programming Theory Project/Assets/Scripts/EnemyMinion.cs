using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMinion : MonoBehaviour
{
    float speed = 50f;

    public GameObject player;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");

    }
        public  void Update()
    {
        if (player != null)
        {
            // look towards player direction
            Vector3 lookDirection = (player.transform.position - transform.position); //.normalized;

            // AddForce to enemyRb with lookDirection times the enemy's speed
            rb.AddForce(lookDirection * speed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Player GameObject not found or inactive.");
        }

    }
}
