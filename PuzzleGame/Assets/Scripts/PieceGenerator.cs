using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    public int pieceNumber;
    private LevelManager levelManager;
    private GameObject piecesParent;
    private GameObject[] pieces;

    public void GenerateThePieces()
    {
        levelManager = GetComponent<LevelManager>();
        pieceNumber = levelManager.thisLevel.pieceNumber;

        piecesParent = new GameObject("PuzzlePieces");
        piecesParent.transform.position = new Vector3(0, -4);

        pieces = new GameObject[pieceNumber];
        for (int i = 0; i < pieceNumber; i++)
        {
            pieces[i] = new GameObject("Piece");
            pieces[i].transform.SetParent(piecesParent.transform, false);
            PuzzlePiece pieceScript = pieces[i].AddComponent<PuzzlePiece>();
            pieceScript.sizeOfTile = gameObject.GetComponent<GridBoardGenerator>().sizeOfTile;
            pieceScript.pieceID = i;
            pieceScript.vertices = levelManager.thisLevel.pieces[i].vertices;
        }
    }
}
