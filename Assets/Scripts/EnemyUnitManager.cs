using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitManager : MonoBehaviour
{
    [SerializeField] int numOfEnemies;
    [SerializeField] int numWhenEnemiesStartMovingSmartly;
    [SerializeField] GameObject enemyPrefab;
    FieldController fieldController;
    List<GameObject> enemies = new List<GameObject>();


    void Start()
    {

        fieldController = GameObject.FindGameObjectWithTag("FieldController").GetComponent<FieldController>();
        CreateEnemies();

    }

    private void CreateEnemies()
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, fieldController.GetAnEmptyTilePosition(), Quaternion.identity);
            enemy.transform.SetParent(transform);
            enemy.GetComponent<EnemyHealth>().EnemyDied += EnemyDeathEventHandler;
            enemies.Add(enemy);
        }
    }

    private void EnemyDeathEventHandler(GameObject enemyWhichDied)
    {
        enemies.Remove(enemyWhichDied);
        if (enemies.Count <= numWhenEnemiesStartMovingSmartly && enemies.Count > 0)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyMovement>().StartMovingSmartly(playerObj);
            }
        }

        if (enemies.Count <= 0)
        {
            GameManager.instance.GameOver(true);
        }
    }
}
