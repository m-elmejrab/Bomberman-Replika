using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldController : MonoBehaviour
{
    int numOfRows;
    int numOfColumns;
    int numOfNonDestructibleObjects;
    int numOfDestructibleObjects;
    [SerializeField] Tile groundTile;
    [SerializeField] Tile nonDestructibleTile;
    [SerializeField] Tile destructibleTile;
    [SerializeField] Tilemap groundMap;
    [SerializeField] Tilemap nonDestructibleMap;
    [SerializeField] Tilemap destructibleMap;
    [SerializeField] int pointsForDestructibleObj;

    void Awake()
    {
        LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        numOfRows = levelManager.GetRowCount();
        numOfColumns = levelManager.GetColumnCount();
        numOfNonDestructibleObjects = levelManager.GetNonDestructiblesCount();
        numOfDestructibleObjects = levelManager.GetDestructiblesCount();

        InitializeMap(groundMap, numOfRows, numOfColumns, Vector3Int.zero);
        InitializeMap(destructibleMap, numOfRows, numOfColumns, Vector3Int.zero);
        InitializeMap(nonDestructibleMap, numOfRows + 2, numOfColumns + 2, new Vector3Int(-1, -1, 0)); //added +2 to create a circular wall around ground map

        groundMap.BoxFill(Vector3Int.zero, groundTile, 0, 0, numOfColumns, numOfRows);
        CreateNonDestructibleObjects();
        CreateDestructibleObjects();
    }

    void InitializeMap(Tilemap map, int rowCount, int columnCount, Vector3Int originPoint)
    {
        map.origin = originPoint;
        map.size = new Vector3Int(columnCount, rowCount, 1);
        map.ResizeBounds();
    }

    void CreateNonDestructibleObjects()
    {
        for (int x = -1; x <= numOfColumns; x++) //create top & bottom wall
        {
            nonDestructibleMap.SetTile(new Vector3Int(x, -1, 0), nonDestructibleTile);
            nonDestructibleMap.SetTile(new Vector3Int(x, numOfRows, 0), nonDestructibleTile);
        }

        for (int y = 0; y < numOfRows; y++) //create left & right wall
        {
            nonDestructibleMap.SetTile(new Vector3Int(-1, y, 0), nonDestructibleTile);
            nonDestructibleMap.SetTile(new Vector3Int(numOfColumns, y, 0), nonDestructibleTile);
        }

        if (numOfNonDestructibleObjects <= 0 || numOfNonDestructibleObjects >= (numOfColumns - 2) * (numOfRows - 2)) //return if num is out of bounds
            return;
        else
        {
            for (int i = 0; i < numOfNonDestructibleObjects; i++)
            {
                bool tileHasBeenPlaced = false;
                while (!tileHasBeenPlaced)
                {
                    int xIndex = Random.Range(1, numOfColumns - 1);
                    int yIndex = Random.Range(1, numOfRows - 1);

                    if (!nonDestructibleMap.HasTile(new Vector3Int(xIndex, yIndex, 0)))
                    {
                        nonDestructibleMap.SetTile(new Vector3Int(xIndex, yIndex, 0), nonDestructibleTile);
                        tileHasBeenPlaced = true;
                    }
                }
            }
        }
    }

    void CreateDestructibleObjects()
    {
        if (numOfDestructibleObjects <= 0 || numOfDestructibleObjects >= (numOfColumns - 2) * (numOfRows - 2) - numOfNonDestructibleObjects) //return if count is out of bounds
            return;
        else
        {
            for (int i = 0; i < numOfDestructibleObjects; i++)
            {
                bool tileHasBeenPlaced = false;
                while (!tileHasBeenPlaced)
                {
                    int xIndex = Random.Range(1, numOfColumns - 1);
                    int yIndex = Random.Range(1, numOfRows - 1);

                    if (!destructibleMap.HasTile(new Vector3Int(xIndex, yIndex, 0)) && !nonDestructibleMap.HasTile(new Vector3Int(xIndex, yIndex, 0)))
                    {
                        destructibleMap.SetTile(new Vector3Int(xIndex, yIndex, 0), destructibleTile);
                        tileHasBeenPlaced = true;
                    }
                }
            }
        }
    }

    public Vector3 GetAnEmptyTilePosition()
    {
        Vector3 emptyPosition = Vector3.zero;
        bool emptyTileFound = false;

        while (!emptyTileFound)
        {
            int xIndex = Random.Range(1, numOfColumns - 1);
            int yIndex = Random.Range(1, numOfRows - 1);

            if (!destructibleMap.HasTile(new Vector3Int(xIndex, yIndex, 0)) && !nonDestructibleMap.HasTile(new Vector3Int(xIndex, yIndex, 0)))
            {
                emptyPosition = groundMap.GetCellCenterLocal(new Vector3Int(xIndex, yIndex, 0));
                emptyTileFound = true;
            }
        }

        return emptyPosition;
    }

    public void TileExploded(Vector3 position)
    {
        Vector3Int intPosition = Vector3Int.FloorToInt(position);
        if (destructibleMap.HasTile(intPosition))
        {
            destructibleMap.SetTile(intPosition, null);
            GameManager.instance.UpdateScore(pointsForDestructibleObj);
        }
    }
}
