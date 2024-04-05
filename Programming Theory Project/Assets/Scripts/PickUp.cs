using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float throwForce = 100;
    float distance;

    public bool canHold = true;
    public bool isHolding = false;

    public GameObject item;
    public GameObject player;

    Vector3 objectPos;


    // void OLDUpdate()
    //  {
    //  if (Input.GetKeyDown("space"))
    //   {
    // GetComponent<Rigidbody>().useGravity = false;
    //  this.transform.position = holdDesti.position;
    //  this.transform.parent = GameObject.Find("Player").transform;
    // Debug.Log("pickup");
    // }
    //  else if (Input.GetKeyUp("space"))
    // {
    //   this.transform.parent = null;
    //    GetComponent<Rigidbody>().useGravity = true;
    // }
    //  }

    void Update()
    {
        //distance = Vector3.Distance(item.transform.position, player.transform);
        if (distance >= 1f)
            isHolding = false;

        if (Input.GetKeyDown(KeyCode.Space) && canHold == true)
        {
            isHolding = true;
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(player.transform);
            Hold();

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Throw();
            }

            else
            {
                objectPos = item.transform.position;
                item.transform.SetParent(null);
                item.GetComponent<Rigidbody>().useGravity = true;
                item.transform.position = objectPos;
            }
        }
    }
    void  Hold()
    {
      if (distance <= 1f)
        {
          
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
            //  this.transform.position = holdDesti.position;
            // this.transform.parent = GameObject.Find("Player").transform;
            Debug.Log("pickup");
        }
      
    }

    void Throw()
    {
        item.GetComponent<Rigidbody>().AddForce(player.transform.forward *throwForce);
        isHolding = false;
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("throw");
    }

}
