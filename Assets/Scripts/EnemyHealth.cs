using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth = 100;
    public int enemyCurrentHealth;
    private EnemyMovement enemyMovement;
    public bool isImmune = false;
    public bool isBoss = false;

    private FloatingHealthBar healthBar;
    public BossHealthBar bossHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
        if(!isBoss)
        {
            healthBar = GetComponentInChildren<FloatingHealthBar>();
        }
        if(isBoss)
        {
            bossHealthBar.BossSetMaxHealth(enemyMaxHealth);
        }

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
        if(!isBoss)
        {
            healthBar.UpdateHealthBar(enemyCurrentHealth, enemyMaxHealth);
            enemyMovement.TakeDamage(modifiedDamage);
        }
        if(isBoss)
        {
            bossHealthBar.BossSetHealth(enemyCurrentHealth);
        }
        DamagePopup.Create(transform.position, (int)modifiedDamage, false);
        if(enemyCurrentHealth <= 0)
        {
            GameManager.Instance.AddPoints(1);
            if(isBoss)
            {
                Destroy(bossHealthBar);
                GameManager.Instance.WinEnemyStage();
            }
            Destroy(gameObject);
        }
    }

}
