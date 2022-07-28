using UnityEngine;

public class PlayerHealth : MonoBehaviour, IKillable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Kill();
        }
    }

    public void Kill()
    {
        GameManager.instance.GameOver(false);
    }
}
