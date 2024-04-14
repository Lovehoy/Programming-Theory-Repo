
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameManager GameManager;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>(); // Find the GameManager instance in the scene
    }
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
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Player"))
        {
            GameManager.GameOver();
            Debug.Log("Player in DeathZone");
            // Destroy the Ammo object
            Destroy(other.gameObject);
        }
    }
    // when player trigger, GameManager.GameOver()
}
