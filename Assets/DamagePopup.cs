using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    //Create a damage popup
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    [SerializeField] private float moveSpeed = 60f;
    [SerializeField] private Vector2 moveDirection = new Vector2(0.7f, 1f);
    [SerializeField] private float directionVariation = 0.2f; // Variation in move direction

    [SerializeField] private float disappearTimerMax = 1f;

    private static int sortingOrder;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;


    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(float damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = disappearTimerMax;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        // Add variation to move direction
        moveDirection += new Vector2(Random.Range(-directionVariation, directionVariation), Random.Range(-directionVariation, directionVariation));
        moveDirection.Normalize();

        moveVector = new Vector3(moveDirection.x, moveDirection.y, 0f) * moveSpeed;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > disappearTimerMax * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
