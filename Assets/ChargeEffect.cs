using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEffect : MonoBehaviour
{
    public GameObject whiteSprite;
    [HideInInspector] public int chargeLevel = 0;
    [HideInInspector] public int previousChargeLevel = 0;

    void Awake()
    {
        
    }
    void Update()
    {
        
        if (chargeLevel > previousChargeLevel)
        {
            StartCoroutine(ExpandWhiteSprite());
        }
        else
        {
            gameObject.SetActive(false);
        }
        previousChargeLevel = chargeLevel;
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
            float scale = Mathf.Lerp(1.3f, 1f, t);
            Color color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));

            whiteSprite.transform.localScale = originalScale * scale;
            whiteSprite.GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }

        whiteSprite.transform.localScale = originalScale;
        whiteSprite.GetComponent<SpriteRenderer>().color = originalColor;

    }
}

