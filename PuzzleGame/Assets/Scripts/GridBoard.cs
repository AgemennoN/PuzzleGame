using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoard : MonoBehaviour
{
    private float boardSize = 5.0f;

    public int boardDim = 5;
    public float sizeOfTile;

    public int[,] gridArray;
    public GameObject[] gridTiles;
    public GameObject gridTilePrefab;

    void Start()
    {
        sizeOfTile = boardSize / boardDim;
        transform.position = new Vector3(-boardSize / 2.0f + sizeOfTile / 2.0f, 0);


        gridArray = new int[boardDim, boardDim];
        gridTiles = new GameObject[boardDim * boardDim];

        for (int y = 0; y < gridArray.GetLength(1); y++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                int Index = x + (y * gridArray.GetLength(0));
                Vector3 tilePos = new Vector3(x * sizeOfTile, y * sizeOfTile, 0);
                gridTiles[Index] = Instantiate(gridTilePrefab, tilePos, Quaternion.identity);
                gridTiles[Index].transform.SetParent(gameObject.transform, false);
                gridTiles[Index].transform.localScale = new Vector3(sizeOfTile, sizeOfTile);
            }
        }
    }
}
