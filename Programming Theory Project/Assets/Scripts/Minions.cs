using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{
    //Parent class gives enemies and player same speed
    public float speed = 20f;
    public Rigidbody minionRb;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {

        // Destroy
        Destroy();

    }

    public void Destroy()
    {
        if (transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
}
