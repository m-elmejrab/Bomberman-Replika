using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IKillable
{
    public event Action<GameObject> EnemyDied;
    [SerializeField] int pointsForKilling;
    private bool isDying = false; //used to stop multiple deaths caused by colliders triggers

    public void Kill()
    {
        if (!isDying)
        {
            isDying = true;
            GameManager.instance.UpdateScore(pointsForKilling);
            EnemyDied?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}
