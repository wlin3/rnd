using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooldownBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text cooldownText;
    private int maxCooldown;
    private int currentCooldown;
    

    public void SetMaxCooldown(float cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = cooldown;
        fill.color = gradient.Evaluate(1f);
        cooldownText.text = cooldown.ToString();
        maxCooldown = cooldown;
        
    }

    public void SetCooldown(int cooldown)
    {
        slider.value = cooldown;

        fill.color = gradient.Evaluate(slider.normalizedValue);
        cooldownText.text = cooldown.ToString();
    }
}
