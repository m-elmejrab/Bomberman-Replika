using UnityEngine;
using System;

public class BombController : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float secondsUntilDetonation;
    [SerializeField] int range;
    public event Action BombExploded;
    float timeSincePlacement = 0f;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1 / secondsUntilDetonation;
    }

    void Update()
    {
        timeSincePlacement += Time.deltaTime;
        if (timeSincePlacement >= secondsUntilDetonation)
            Detonate();
    }

    private void Detonate()
    {
        BombExploded?.Invoke();

        for (int x = -range; x <= range; x++)
        {
            int yRange = range - Math.Abs(x);

            if (yRange == 0)
                Instantiate(explosionPrefab, transform.position + new Vector3(x, 0f, 0f), Quaternion.identity);
            else
            {
                for (int y = -yRange; y <= yRange; y++)
                {
                    Instantiate(explosionPrefab, transform.position + new Vector3(x, y, 0f), Quaternion.identity);
                }
            }
        }

        Destroy(gameObject);
    }
}
