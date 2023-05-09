using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text healthText;
    private int maxHealth;
    

    public void BossSetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        healthText.text = health.ToString() + "/" + health.ToString();
        maxHealth = health;
        
    }

    public void BossSetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
    }
}
