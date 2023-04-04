using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public bool isMovingRight = true;
    public float resetPosition = -20f;

    private Transform[] backgrounds;
    private int movingRight = 1;

    private void Start()
    {
        // Get all the background images
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        // Move the backgrounds
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float moveAmount = scrollSpeed * Time.deltaTime * movingRight;
            Vector3 newPosition = backgrounds[i].position + new Vector3(moveAmount, 0, 0);

            backgrounds[i].position = newPosition;

            // Check if we need to reset the position of the background
            if ((isMovingRight && backgrounds[i].position.x > resetPosition) ||
                (!isMovingRight && backgrounds[i].position.x < resetPosition))
            {
                float offset = backgrounds.Length * Mathf.Abs(resetPosition);
                backgrounds[i].position += new Vector3(offset * -movingRight, 0, 0);
            }
        }
    }
}
