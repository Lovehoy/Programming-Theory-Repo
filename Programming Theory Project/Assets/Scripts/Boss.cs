using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    BarrelSpawnManager barrelSpawnManager;

    [SerializeField] private Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        barrelSpawnManager = FindObjectOfType<BarrelSpawnManager>();
        if (barrelSpawnManager != null)
        {
            barrelSpawnManager.BarrelSpawned += OnBarrelSpawned; // Subscribe to the event
        }
    }

    private void Update()
    {
        // Check if any other animation is playing
        bool isAnimating = animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;

        // Set the "Idle" parameter based on whether any other animation is playing
        animator.SetBool("Idle", !isAnimating);
    }
    void OnBarrelSpawned()
    {
        animator.SetTrigger("Throw"); // Play the "Throw" animation
    }

}
