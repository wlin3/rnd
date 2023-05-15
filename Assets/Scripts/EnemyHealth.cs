using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth = 100;
    public int enemyCurrentHealth;
    private EnemyMovement enemyMovement;
    public bool isImmune = false;

    private FloatingHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth > enemyMaxHealth)
        {
            enemyCurrentHealth = enemyMaxHealth;
        }
    }

    public void TakeDamageEnemy(float damage)
    {
        float variation = UnityEngine.Random.Range(-0.15f, 0.25f);
        float modifiedDamage = damage + damage * variation;
        enemyCurrentHealth -= (int)modifiedDamage;
        healthBar.UpdateHealthBar(enemyCurrentHealth, enemyMaxHealth);
        enemyMovement.TakeDamage(modifiedDamage);
        DamagePopup.Create(transform.position, (int)modifiedDamage, false);
        if(enemyCurrentHealth <= 0)
        {
            GameManager.Instance.AddPoints(1);
            Destroy(gameObject);
        }
    }

}
