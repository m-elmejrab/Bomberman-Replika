using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy"))
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Debug.Log("Player Died");
    }
}
