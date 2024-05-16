using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.IO;



// conditional instructions to the compiler, namespace will only be included when you are compiling within the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif
// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
// ************* Main manages scenes and menus *****************

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    // ****** for Pause *******
    private bool isPaused = false;
    public GameObject pauseText;
    public GameObject MainMenuButton;
    public GameObject RestartButton;
    private void Awake()
    {
        //if there are 2 MainManagers in scene, destroy this MainManager
        //Happens when the other scene you moved to tries to creates its own MainManager 
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        //But if this is the only one, don't destroy it
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Change KeyCode.Escape to your desired pause button
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {

        // ############## Add a disable to Pause if in Main Menu (scene 0)
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; // Freeze time if paused, resume time if unpaused

        if (pauseText != null)
        {
            pauseText.SetActive(isPaused);
            MainMenuButton.SetActive(isPaused);
            RestartButton.SetActive(isPaused);
        }

        // Disable/enable player input
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = !isPaused;
        }
    }

    // **************** LOADING LEVELS ***********************
    public void NextLevel()
    {
        if (isPaused)
        { TogglePause(); }
        // Get the index of the current active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if the current scene is the last scene in the build settings
        if (currentSceneIndex >= SceneManager.sceneCountInBuildSettings - 1)
        {
            // Load the first scene
            SceneManager.LoadScene(0);
        }
        else
        {
            // Load the next scene by incrementing the current scene index
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    // public void StartGame()
    // {
    //     SceneManager.LoadScene(1);
    // }

    public void RestartScene()
    {
        if (isPaused)
        { TogglePause(); }
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Reload the current scene
        SceneManager.LoadScene(currentSceneName);
    }
    public void BackToMenu()
    {
        if (isPaused)
        { TogglePause(); }
        SceneManager.LoadScene(0);
        //ensure your scnees are numbered right in File > Build Settings
    }
    // put Pause back in
    public void ExitGame()
    {
#if UNITY_EDITOR
        // then keep the EditorApplication.ExitPlaymode() code and discard the Application.Quit
        EditorApplication.ExitPlaymode();

        //elseplayer is built, UNITY_EDITOR will be false
#else

Application.Quit();

#endif
    }
}
