﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI textHealth;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        textHealth.text = health.ToString();

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        textHealth.text = health.ToString();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
