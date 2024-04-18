using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static bool s_permanentInstance = false; // set to true if you ever only want one instance among all scenes and drop an instance of GameManager in your first scene you load
    public bool oneShotAwarded = false;
    public bool gameOver = false;

    public static GameManager Instance { get; private set; }

    public int maxPoints = 10;
    public int minPoints = 0;
    public int currentPoints;

    public Text ScoreText;

    public GameObject gameOverText;
    public GameObject restartButton;

    public OneShotBar oneShotBar;
    public PlayerController playerController;

    public GameObject playerPrefab; // Reference to the player prefab

    private GameObject playerInstance; // Instance of the player GameObject
    // Start is called before the first frame update
    private void Awake()
    {
        if (s_permanentInstance == true)
        {
            if (Instance != null)
            {
              //Debug.LogError("GameManager initialized twice, overwriting");
            }
            DontDestroyOnLoad(this);  // prevents this from being destroyed on scene load
        }
        // Instantiate player
        playerInstance = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        
    }

    void Start()
    {
     //oneShotAwarded = false;
 //  currentPoints = minPoints;
        oneShotBar = FindObjectOfType<OneShotBar>();
        if (oneShotBar == null)
        {
            Debug.LogError("One Shot Bar not found by game manager!");
            return;
        }
       
        oneShotBar.SetMinPoints(minPoints);

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            // Get the PlayerController component attached to the player GameObject
            playerController = player.GetComponent<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("PlayerController component not found on the player GameObject");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found");
        }
    }

    private void Update()
    {
        if (currentPoints >= maxPoints && !oneShotAwarded)
        {
            AwardOneShot(maxPoints);
            Debug.Log("Update passsed Award One Shot");
        }
    }
    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        gameOver = true;
    }

    public void AwardPoints( int point)
    { 
        if (currentPoints  < maxPoints)
        {
            currentPoints += point;
            oneShotBar.SetPoints(currentPoints);
            // You can add more logic here if needed, such as updating UI to display the new total points
            Debug.Log("Points awarded: " + point + ", Total points: " + currentPoints);
        }
        
    }

    public void AwardOneShot( int point) 
    { 
        // make bar flash?? oneShotBar.SetPoints(currentPoints);
        // You can add more logic here if needed, such as updating UI to display the new total points
        playerController.ShootOneShot(); 
        Debug.Log("Game Controller Awarded One Shot");
        oneShotAwarded = true;
    }
}
