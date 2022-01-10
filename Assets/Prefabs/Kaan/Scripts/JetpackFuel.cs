using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetpackFuel : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxFuel(float fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel; 
    }

    public void GetJetpackFuel(float fuel)
    {
        slider.value = fuel;
    }
}
