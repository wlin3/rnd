using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
         if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamageEnemy(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Damaged Enemy");
        if(currentHealth <= 0)
        {
            currentHealth = maxHealth;
            Debug.Log(currentHealth.ToString());
        }
    }
}
