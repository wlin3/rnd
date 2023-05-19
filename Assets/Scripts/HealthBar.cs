using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider chipSlider;
    public Gradient gradient;
    public Image healthFill;
    public Image chipFill;
    public TMP_Text healthText;
    private int maxHealth;

    private Coroutine chipEffectCoroutine;

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        chipSlider.maxValue = health;
        chipSlider.value = health;
        healthFill.color = gradient.Evaluate(1f);
        healthText.text = health.ToString() + "/" + health.ToString();
        maxHealth = health;
    }

    public void SetHealth(int health)
    {
        if (chipEffectCoroutine != null)
        {
            StopCoroutine(chipEffectCoroutine);
        }

        if (chipSlider.value > health)
        {
            // Adjust the target value of the existing coroutine
            chipEffectCoroutine = StartCoroutine(ChipEffectCoroutine(chipSlider.value, health));
        }
        else
        {
            // Start a new coroutine with the current value as the starting value
            chipEffectCoroutine = StartCoroutine(ChipEffectCoroutine(health, health));
        }

        healthSlider.value = health;
        healthFill.color = gradient.Evaluate(healthSlider.normalizedValue);
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    private IEnumerator ChipEffectCoroutine(float startValue, float targetValue)
    {
        float delayDuration = 0.5f; // Delay before starting the decay effect
        float decayDuration = 0.5f; // Adjust the duration as needed
        Color chipColor = Color.red;
        chipFill.color = Color.red;

        yield return new WaitForSeconds(delayDuration);

        float elapsedTime = 0f;

        while (elapsedTime < decayDuration)
        {
            float currentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / decayDuration);
            chipSlider.value = currentValue;
            chipFill.color = Color.red;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        chipSlider.value = targetValue;
        chipFill.color = Color.red;


        chipEffectCoroutine = null;
    }
}
