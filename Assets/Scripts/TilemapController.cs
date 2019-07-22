using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    Tilemap myMap;
    static int worldSize = 5;
    public TileBase voidTile;
    Vector3Int startTile;
    TileBase[,] originalTiles;
    bool[,] revealedTiles;

    // Start is called before the first frame update
    void Start()
    {
        originalTiles = new TileBase[worldSize, worldSize];
        myMap = GetComponent<Tilemap>();
        Debug.Log("" + myMap.origin + ", " + myMap.size);
        int minX = int.MaxValue;
        int maxY = int.MinValue;
        for (int i = myMap.origin.x; i < myMap.size.x; i++)
        {
            for (int j = myMap.origin.y; j < myMap.size.y; j++)
            {
                if (myMap.HasTile(new Vector3Int(i, j, 0)))
                {
                    if (i < minX) { minX = i; }
                    if (j > maxY) { maxY = j; }
                }
            }
        }
        startTile = new Vector3Int(minX, maxY, 0);
        for (int i = 0; i < worldSize; i++)
        {
            for (int j = 0; j < worldSize; j++)
            {
                Vector3Int pos = new Vector3Int(startTile.x + i, startTile.y - j, 0);
                originalTiles[i, j] = myMap.GetTile(pos);
                if (!(i == 0 && j == 0))
                {
                    myMap.SetTile(pos, voidTile);
                    Debug.Log("" + pos);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealTile(Vector2Int relTilePos)
    {
        Vector3Int absTilePos = startTile;
        absTilePos.x += relTilePos.x;
        absTilePos.y -= relTilePos.y;
        TileBase newTile = originalTiles[relTilePos.x, relTilePos.y];
        myMap.SetTile(absTilePos, newTile);
    }
}
