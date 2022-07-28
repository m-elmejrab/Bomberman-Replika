using UnityEngine;

public class PlayerBombController : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    bool canPlaceBomb = true;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && canPlaceBomb && !GameManager.instance.IsGamePaused())
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
        SoundManager.instance.PlayBombPlacementSound();
    }

    private void explosionEventHandler()
    {
        canPlaceBomb = true;
        SoundManager.instance.PlayExplosionSound();
    }
}
