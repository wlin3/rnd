using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        

        if (Input.GetKeyDown(KeyCode.U))
        {
            GameManager.Instance.DeleteSaveFully();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.Instance.DeleteRunData();
        }
    }

    public void TakeDamage(int damage)
    {
        float variation = UnityEngine.Random.Range(-0.15f, 0.1f);
        float modifiedDamage = damage + damage * variation;
        currentHealth -= (int)modifiedDamage;
        DamagePopup.Create(transform.position, (int)modifiedDamage, true);
        //Debug.Log("Damage");
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            //Destroy(player);
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }

}
