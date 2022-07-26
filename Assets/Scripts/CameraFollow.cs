using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    bool playerIsActive = false;
    GameObject player;
    
    void Start() 
    {
        LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        levelManager.PlayerInstantiated += StartFollowingPlayer;
    }

    private void StartFollowingPlayer(GameObject playerObject)
    {
        player = playerObject;
        playerIsActive = true;
    }

    void Update()
    {
        if(playerIsActive)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z );
    }
}
