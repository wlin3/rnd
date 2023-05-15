using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voiding : MonoBehaviour
{
    public float threshold = -15f;

    void Update()
    {
        if (transform.position.y < threshold)
        {
            if(gameObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.currentHealth = 0;
                }
            }

            else
            {
                Destroy(gameObject);
            }

        }
    }
}
