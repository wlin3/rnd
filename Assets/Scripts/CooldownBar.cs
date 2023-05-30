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

    private Vector3 initialRotation;


    private void Awake()
    {
        if(maxCooldown <= 0f)
        {
            gameObject.SetActive(false);
        }

        if(cooldownText == null)
        {
            initialRotation = transform.rotation.eulerAngles;
        }
    }
    public void SetMaxCooldown(float cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = cooldown;
        fill.color = gradient.Evaluate(1f);
        if(cooldownText != null)
        {
            cooldownText.text = cooldown.ToString();
        }

        maxCooldown = cooldown;
        
    }

    public void SetCooldown(float cooldown)
    {
        slider.value = cooldown;

        fill.color = gradient.Evaluate(slider.normalizedValue);
        if(cooldownText != null) 
        {
           cooldownText.text = cooldown.ToString("F2"); // Round to 2 decimal places

        }

        if (cooldown <= 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(cooldownText == null)
        {
            // Keep the health bar facing towards the camera
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

            // Set the rotation of the health bar to the initial rotation
            transform.rotation *= Quaternion.Euler(initialRotation);
        }
    }

}
