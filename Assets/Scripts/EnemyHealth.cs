using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth = 100;
    public int enemyCurrentHealth;
    private EnemyMovement enemyMovement;
    public bool isImmune = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
         if (enemyCurrentHealth > enemyMaxHealth)
         {
            enemyCurrentHealth = enemyMaxHealth;
         }
    }

    public void TakeDamageEnemy(int damage)
    {
        enemyCurrentHealth -= damage;
        //Debug.Log("Damaged Enemy");
        enemyMovement.TakeDamage(damage);
        if(enemyCurrentHealth <= 0)
        {
            GameManager.Instance.AddPoints(1);
            Destroy(gameObject);
        }
    }
}
