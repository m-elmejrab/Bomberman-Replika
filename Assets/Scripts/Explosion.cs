using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent<IKillable>(out IKillable killable))
        {
            killable.Kill();
        }
        else if(other.CompareTag("DestuctibleTiles"))
        {
            FieldController fieldController = other.GetComponentInParent<FieldController>();
            fieldController.TileExploded(transform.position);
        }
    }

}
