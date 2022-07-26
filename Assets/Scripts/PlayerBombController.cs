using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombController : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    bool canPlaceBomb = true;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && canPlaceBomb)
        {
            PlaceBomb();
        }
    }

    private void PlaceBomb()
    {
        canPlaceBomb = false;
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.BombExploded += explosionEventHandler;        
    }

    private void explosionEventHandler()
    {
        canPlaceBomb = true;
    }
}
