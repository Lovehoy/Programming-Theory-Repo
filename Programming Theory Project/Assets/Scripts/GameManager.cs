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
    public bool levelWon = false;


    public static GameManager Instance { get; private set; }

    private int maxPoints = 5;
    private int minPoints = 0;
    public int currentPoints;

    public Text ScoreText;
    public GameObject winLevelText;


    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject nextButton;
    

    public OneShotBar oneShotBar;
    public PlayerController playerController;

    public GameObject playerPrefab; 
    public GameObject boss;

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
        playerInstance = Instantiate(playerPrefab, transform.position, transform.rotation);

    }

    void Start()
    {
     //oneShotAwarded = false;
 //  currentPoints = minPoints;
        

        GameObject player = GameObject.FindWithTag("Player");
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

        oneShotBar = FindObjectOfType<OneShotBar>();
        if (oneShotBar == null)
        {
            Debug.LogError("One Shot Bar not found by game manager!");
            return;
        }

        oneShotBar.SetMinPoints(minPoints);
        GameObject boss = GameObject.FindWithTag("Boss");
    }

    private void Update()
    {
        if (currentPoints >= maxPoints && !oneShotAwarded)
        {
            AwardOneShot(maxPoints);
            Debug.Log("Update passsed Award One Shot");
        }

        if (playerPrefab = null)
        {
            GameOver();
        }

     if (boss = null)
        {
            GameOver();
        }
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

    public void AwardOneShot( int maxPoints) 
    { 
        // make bar flash?? oneShotBar.SetPoints(currentPoints);
        // You can add more logic here if needed, such as updating UI to display the new total points
        playerController.HasAward(); 
        Debug.Log("Game Controller Awarded One Shot");
        oneShotAwarded = true;
    }

    public void ResetPoints()
    {
        oneShotBar.SetPoints(0);
        currentPoints = minPoints;
        oneShotAwarded = false;

      //  Barrel.StopBurning();
      //  OnOneShotAwardedChanged.Invoke(oneShotAwarded);
    }
    // ****************** WIN ****************************
    public void WinLevel()
    {
        winLevelText.SetActive(true);
        nextButton.SetActive(true);
        levelWon = true;

    }
    // ****************** GAME OVER ****************************
    public void GameOver()
    { if (!levelWon)
        {
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            gameOver = true;
        }

    }
}
