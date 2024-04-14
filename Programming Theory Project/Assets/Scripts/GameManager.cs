using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

    
{
    public bool gameOver = false;
    public GameObject gameOverText;
    public GameObject restartButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        gameOver = true;
    }
}
