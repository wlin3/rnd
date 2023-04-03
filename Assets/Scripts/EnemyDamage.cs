using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    PlayerHealth playerHealth;
    EnemyMovement enemyMovement;

    Player player;
    void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        player = GameObject.Find("Player").GetComponent<Player>();
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
            enemyMovement.EnemyRetreat();
        }
    }

}
