using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    void Start()
    {
        // Subscribe once
        PlayerHealth.instance.onHealthChanged.AddListener(UpdateBar);

        // Init bar to full
        float max = PlayerHealth.instance.maxHealth;
        UpdateBar(max, max);

        // If using slider set its max here
        if (slider) slider.maxValue = max;
    }

    void UpdateBar(float cur, float max)
    {
        if (slider) slider.value = cur;
        if (fill) fill.fillAmount = cur / max;
    }
}


