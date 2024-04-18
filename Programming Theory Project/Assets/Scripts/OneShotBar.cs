using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

// tutorial @ Brackeys
public class OneShotBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    GameManager gameManager;

    public int minPoints = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
            return;
        }

       // SetMinPoints(minPoints);
        // Now you can use the gameManager reference to access GameManager's methods or properties
    }
    public void SetMinPoints(int point)
    {
        slider.minValue = point;
        slider.value = point;

       fill.color = gradient.Evaluate(2f);
    }
    public void SetPoints(int point)
    {
        slider.value = point;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}


