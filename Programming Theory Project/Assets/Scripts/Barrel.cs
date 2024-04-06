using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
    
{
    public Rigidbody rb;
    
    bool _grounded = false; // _grounded = isGrounded
    bool wasGrounded;
    bool wasFalling;
    
    float startOfFall;
    float minFall = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGround();
        if (!wasGrounded && _grounded) Break();
        if (!wasFalling && IsFalling) startOfFall = transform.position.y;
        

        wasGrounded = _grounded;
        wasFalling = IsFalling;
    }

    private void Break ()
    {
        float fallDistance = startOfFall - transform.position.y;

        if (fallDistance < minFall) 
        {
            Debug.Log(name + "Fell" + (fallDistance) + "units");
        }
     

    }

    void CheckGround()
    {
        _grounded = Physics.Raycast(rb.transform.position + Vector3.up, -Vector3.up, 1f);
    }

    bool IsFalling { get { return (_grounded && rb.velocity.y < 0); } }
}
