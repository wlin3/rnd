using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public GameObject prefab;
    private List<Transform> enemies = new List<Transform>();
    private Dictionary<Transform, GameObject> enemyToPointer = new Dictionary<Transform, GameObject>();

    private void Start()
    {
        InvokeRepeating(nameof(UpdateEnemies), 0f, 1f);
    }

    private void UpdateEnemies()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyObjects)
        {
            if (enemy.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (!enemies.Contains(enemy.transform))
                {
                    enemies.Add(enemy.transform);
                }

                if (!enemyToPointer.ContainsKey(enemy.transform))
                {
                    GameObject pointerAdded = new GameObject("pointerAdded");
                    pointerAdded.transform.parent = enemy.transform;
                    enemyToPointer.Add(enemy.transform, pointerAdded);

                    SpawnPointer(enemy.transform);
                }
            }
            else
            {
                if (enemies.Contains(enemy.transform))
                {
                    enemies.Remove(enemy.transform);
                }

                if (enemyToPointer.ContainsKey(enemy.transform))
                {
                    GameObject pointerAdded = enemyToPointer[enemy.transform];
                    enemyToPointer.Remove(enemy.transform);
                    Destroy(pointerAdded);
                }
            }
        }
    }

    private void SpawnPointer(Transform targetEnemy)
    {
        GameObject pointerObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        pointerObject.transform.parent = GameObject.Find("Player").transform;
        pointerObject.transform.localPosition = Vector3.zero;
        PointerScript pointerScript = pointerObject.GetComponent<PointerScript>();
        pointerScript.Target = targetEnemy;
        enemyToPointer[targetEnemy] = pointerObject;
    }

}

