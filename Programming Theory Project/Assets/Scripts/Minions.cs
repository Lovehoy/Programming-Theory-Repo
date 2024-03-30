using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{
    //Parent class gives enemies and player same speed

    [Header("Rigidbody")]
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    // smae for all children (movement differences in FixedUpdate)
    void Update()
    {
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
