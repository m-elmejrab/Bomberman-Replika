using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IKillable
{
    public event Action<GameObject> EnemyDied;
    public void Kill()
    {
        EnemyDied?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
