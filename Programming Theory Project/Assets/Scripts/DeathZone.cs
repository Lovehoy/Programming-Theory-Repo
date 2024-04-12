
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // add tag check. code may be used to destory enemies and barrels for zone and ammo

        // Destroy the other object
        Destroy(other.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            Debug.Log("Hit DeathZone");
            // Destroy the Ammo object
            Destroy(other.gameObject);
        }
    }
}
