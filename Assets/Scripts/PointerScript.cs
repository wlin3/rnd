using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    public Transform Target;
    public float disableDistance = 5f; // distance at which to disable the pointer
    public float fadeDistance = 8f; // distance at which to start fading the pointer
    public float fadeSpeed = 1f; // speed at which the pointer fades in/out
    public float minScale = 0.3f; // minimum scale of the pointer sprite

    private SpriteRenderer pointerSprite; // reference to the pointer sprite renderer
    private float initialAlpha; // initial alpha value of the sprite renderer

    void Start()
    {
        pointerSprite = GetComponentInChildren<SpriteRenderer>();
        initialAlpha = pointerSprite.color.a;
    }

    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        var dir = Target.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (dir.magnitude <= disableDistance)
        {
            pointerSprite.color = new Color(pointerSprite.color.r, pointerSprite.color.g, pointerSprite.color.b, 0f);
            pointerSprite.transform.localScale = new Vector3(minScale, minScale, 1f);
            pointerSprite.transform.localPosition = new Vector3(5f, pointerSprite.transform.localPosition.y, pointerSprite.transform.localPosition.z);
        }
        else if (dir.magnitude <= fadeDistance)
        {
            float alpha = Mathf.Lerp(0f, initialAlpha, (dir.magnitude - disableDistance) / (fadeDistance - disableDistance));
            pointerSprite.color = new Color(pointerSprite.color.r, pointerSprite.color.g, pointerSprite.color.b, alpha);
            float scale = Mathf.Lerp(minScale, 0.5f, (dir.magnitude - disableDistance) / (fadeDistance - disableDistance));
            pointerSprite.transform.localScale = new Vector3(scale, scale, 1f);
            float posX = Mathf.Lerp(5f, 2f, (fadeDistance - dir.magnitude) / (fadeDistance - disableDistance));
            pointerSprite.transform.localPosition = new Vector3(posX, pointerSprite.transform.localPosition.y, pointerSprite.transform.localPosition.z);
        }
        else
        {
            pointerSprite.color = new Color(pointerSprite.color.r, pointerSprite.color.g, pointerSprite.color.b, initialAlpha);
            pointerSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
            pointerSprite.transform.localPosition = new Vector3(5f, pointerSprite.transform.localPosition.y, pointerSprite.transform.localPosition.z);
        }
    }
}
