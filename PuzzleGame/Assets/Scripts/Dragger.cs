using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 dragOffset; // Mouse position according to the piece to drag
    private float sizeOfTile;   // To position the piece that placed on the grid
    private GameObject LevelManager;
    private GameObject PuzzlePieceParent;
    private int numberOfPieces;
    private int pieceId;
    public bool isPlaced; // To end the level all pieces must be placed

    void Start()
    {
        LevelManager = GameObject.Find("LevelManager");
        PuzzlePieceParent = GameObject.Find("PuzzlePieces");
        sizeOfTile = LevelManager.GetComponent<GridBoardGenerator>().sizeOfTile;
        numberOfPieces = LevelManager.GetComponent<PieceGenerator>().pieceNumber;

        mainCam = Camera.main;
    }

    private void OnMouseDown()
    {
        // After the level is done Dragger component becomes disabled 
        //and player cant move the pieces.
        if (enabled == true) 
        {
            pieceId = gameObject.GetComponent<PuzzlePiece>().pieceID;
            for (int i = 0; i < PuzzlePieceParent.transform.childCount; i++)
            {
                PuzzlePieceParent.transform.GetChild(i).GetComponent<PuzzlePiece>().SetOrder(pieceId, numberOfPieces);
            }

            //Without the dragOffset when a piece is Dragged its position transform centered to the mouse position
            //to prevent that offset is calculated to measure the position of mouse according to the piece
            dragOffset = transform.position - GetMousePosition();
            isPlaced = false;
        }
    }

    private void OnMouseDrag()
    {        
        // After the level is done Dragger component becomes disabled 
        //and player cant move the pieces.
        if (enabled == true) 
        {
            transform.position = dragOffset + GetMousePosition();
        }
    }

    private void OnMouseUp()
    {
        isPlaced = true;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    // If placed on a grid, rounds its position according to the grid's position and size
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "GridTileSensor")
        {
            if (isPlaced == true)
            {
                Vector3 pos = gameObject.transform.position;
                Vector3 boardPos = GameObject.Find("GridBoard").transform.position;
                Vector3 tileOffset = new Vector3(sizeOfTile/2, sizeOfTile / 2);

                pos += -boardPos + tileOffset;
                pos = new Vector3(Mathf.Round(pos.x / sizeOfTile) * sizeOfTile, Mathf.Round(pos.y / sizeOfTile) * sizeOfTile, pos.z);
                gameObject.transform.position = pos + boardPos - tileOffset;
            }
        }
    }



}
