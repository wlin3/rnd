using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEffect : MonoBehaviour
{
    public GameObject whiteSprite;
    private Coroutine currentAnimation;

    [HideInInspector]public int chargeLevel = 58;
    [HideInInspector] public int lastChargeLevel = 58;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void PlayAnimation()
    {
        if(chargeLevel != lastChargeLevel)
        {
            // Start the new animation
            currentAnimation = StartCoroutine(ExpandWhiteSprite());
            lastChargeLevel = chargeLevel;
        }
        
    }

    IEnumerator ExpandWhiteSprite()
    {
        gameObject.SetActive(true);
        float duration = 0.1f; // The duration of the effect in seconds
        float startTime = Time.time;
        Vector3 originalScale = whiteSprite.transform.localScale;
        Color originalColor = whiteSprite.GetComponent<SpriteRenderer>().color;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            float scale = Mathf.Lerp(0.9f, 1f, t);
            Color color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));

            whiteSprite.transform.localScale = originalScale * scale;
            whiteSprite.GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }

        whiteSprite.transform.localScale = originalScale;
        whiteSprite.GetComponent<SpriteRenderer>().color = originalColor;
        currentAnimation = null;
        gameObject.SetActive(false);
    }
}
