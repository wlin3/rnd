using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool BossisImmune = false;

    public BossHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.BossSetMaxHealth(maxHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "arrow")
        {
            Debug.Log("Arrow hit boss");
            BossTakeDamage(10);
            Destroy(collision.gameObject);
        }
    }

    public void BossTakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.BossSetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            currentHealth = maxHealth;
            healthBar.BossSetHealth(currentHealth);
        }
    }
}
