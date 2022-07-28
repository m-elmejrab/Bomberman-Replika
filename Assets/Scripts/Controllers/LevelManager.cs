using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class LevelManager : MonoBehaviour
{
    public event Action<GameObject> PlayerInstantiated;
    [SerializeField] int numOfRows;
    [SerializeField] int numOfColumns;
    [SerializeField] int numOfNonDestructibleObjects;
    [SerializeField] int numOfDestructibleObjects;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Tilemap groundMap;

    GameObject player;

    void Start()
    {
        player = Instantiate(playerPrefab, groundMap.GetCellCenterLocal(new Vector3Int(0, numOfRows - 1, 0)), Quaternion.identity);
        PlayerInstantiated?.Invoke(player);
    }

    public int GetRowCount()
    {
        return numOfRows;
    }

    public int GetColumnCount()
    {
        return numOfColumns;
    }

    public int GetDestructiblesCount()
    {
        return numOfDestructibleObjects;
    }

    public int GetNonDestructiblesCount()
    {
        return numOfNonDestructibleObjects;
    }
}
