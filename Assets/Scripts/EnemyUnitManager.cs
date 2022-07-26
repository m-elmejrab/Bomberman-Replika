using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitManager : MonoBehaviour
{
    [SerializeField] int numOfEnemies;
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
            enemies.Add(enemy);
        }
    }
}
