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
public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

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
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        //ensure your scnees are numbered right in File > Build Settings
    }
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
