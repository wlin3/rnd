using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public float difficultyMultiplier = .05f;
    PlayerHealth playerHealth;
    EnemyMovement enemyMovement;
    private int numberOfWins;
    private int trueDamage;

    Player player;
    void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        player = GameObject.Find("Player").GetComponent<Player>();
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        numberOfWins = GameManager.Instance.GetWins();
    }
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            trueDamage = Mathf.RoundToInt(damage * (1f + (difficultyMultiplier * numberOfWins)));
            playerHealth.TakeDamage(trueDamage);
            enemyMovement.EnemyRetreat();
        }
    }

}
