using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Gradient gradient;


    private Vector3 initialRotation;

    private void Start()
    {
        // Find the slider component on this object
        slider = GetComponentInChildren<Slider>();

        // Store the initial rotation of the health bar
        initialRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        // Keep the health bar facing towards the camera
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        
        // Set the rotation of the health bar to the initial rotation
        transform.rotation *= Quaternion.Euler(initialRotation);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        // Calculate normalized health value
        float normalizedValue = Mathf.Clamp01(currentValue / maxValue);

        // Evaluate color at that point in the gradient
        slider.fillRect.GetComponentInChildren<Image>().color = gradient.Evaluate(normalizedValue);

        // Set slider value
        slider.value = normalizedValue;
    }

}
