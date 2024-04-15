using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

    
{

    public int maxPoints = 10;
    public int minPoints = 0;
    public int currentPoints;

    public Text ScoreText;

    public bool gameOver = false;
    public GameObject gameOverText;
    public GameObject restartButton;

    public OneShotBar oneShotBar;
    // Start is called before the first frame update
    void Start()
    {
        oneShotBar = FindObjectOfType<OneShotBar>(); // Find the OneShotBar instance in the scene
        currentPoints = minPoints;
        oneShotBar.SetMinPoints(minPoints);
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

    public void AwardOneShot( int points ) 
    {
        if (currentPoints >= maxPoints)
        {
            // make bar flash?? oneShotBar.SetPoints(currentPoints);
            // You can add more logic here if needed, such as updating UI to display the new total points

            Debug.Log("Award One Shot");
        }

    }
}
