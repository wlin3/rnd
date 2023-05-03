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
    private float maxCooldown;
    private float currentCooldown;
    
    private void Awake()
    {
        if(maxCooldown <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetMaxCooldown(float cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = cooldown;
        fill.color = gradient.Evaluate(1f);
        cooldownText.text = cooldown.ToString();
        maxCooldown = cooldown;
        
    }

    public void SetCooldown(float cooldown)
    {
        slider.value = cooldown;

        fill.color = gradient.Evaluate(slider.normalizedValue);
        cooldownText.text = cooldown.ToString("F2"); // Round to 2 decimal places

        if (cooldown <= 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
