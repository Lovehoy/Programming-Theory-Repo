using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMinionController : Minions
{
    //public Rigidbody playerRb;
    private Vector3 direction; 
    // Start is called before the first frame update
    void Start()
    {
      //  playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // get inputs
        direction.x = Input.GetAxis("Horizontal") * speed;
        direction.y = Input.GetAxis("Vertical") * speed;
    }

    public override void FixedUpdate()
    {
        // free move
        minionRb.MovePosition(minionRb.position + direction * Time.fixedDeltaTime);
        Destroy();
    }
}
