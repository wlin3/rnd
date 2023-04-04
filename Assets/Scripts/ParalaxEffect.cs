using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        float offsetX = deltaMovement.x * parallaxEffectMultiplier.x;
        transform.position += new Vector3(offsetX, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;

        float threshold = textureUnitSizeX * transform.localScale.x;
        if (Mathf.Abs(offsetX) >= threshold)
        {
            float offsetPositionX = (offsetX > 0 ? 1 : -1) * (threshold - Mathf.Abs(offsetX));
            transform.position += new Vector3(offsetPositionX, 0f, 0f);
        }
    }
}
