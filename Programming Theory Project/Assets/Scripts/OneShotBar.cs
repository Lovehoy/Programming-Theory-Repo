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
    // Start is called before the first frame update
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
