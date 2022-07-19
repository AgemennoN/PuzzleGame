using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoardGenerator : MonoBehaviour
{
    public GameObject gridTilePrefab;//Prefab of the Grid tiles
    public GameObject[] gridTiles;  // Grid Tiles array
    private float boardSize = 5.0f; // a fixed value for GridBoards size
    private float boardPosXOffset;  
    private float boardPosYOffset;  

    public int boardDim;            // Pulls from LevelManager(reads from Jsonfile)
    public float sizeOfTile;        // Calculated according to boardSize and boardDim 
    private Vector3 boardPos;       // Calculated according to boardSize and sizeOfTile
    private GameObject gridBoard;   // Parent object to hold the tiles.

    public void GenerateTheGrid()
    {
        gridBoard = new GameObject("GridBoard"); // New Emty Parent object to hold all the GridTiles

        boardDim = GetComponent<LevelManager>().thisLevel.gridDim;
        sizeOfTile = boardSize / boardDim;

        boardPosXOffset = -2.5f + (sizeOfTile / 2.0f);
        boardPosYOffset = -0.5f + (sizeOfTile / 2.0f);
        boardPos = new Vector3(boardPosXOffset, boardPosYOffset, 0.1f);
        gridBoard.transform.position = boardPos;

        gridTiles = new GameObject[boardDim * boardDim];

        for (int y = 0; y < boardDim; y++)
        {
            for (int x = 0; x < boardDim; x++)
            {
                int Index = x + (y * boardDim);
                Vector3 tilePos = new Vector3(x * sizeOfTile, y * sizeOfTile, 0);
                gridTiles[Index] = Instantiate(gridTilePrefab, tilePos, Quaternion.identity);
                gridTiles[Index].transform.SetParent(gridBoard.transform, false);
                gridTiles[Index].transform.localScale = new Vector3(sizeOfTile, sizeOfTile);
            }
        }
    }
}

