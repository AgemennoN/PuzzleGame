using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Vector2[] vertices;
    public float sizeOfTile;
    public int pieceID;

    private Rigidbody2D Rb;
    private PolygonCollider2D pCollider;
    private Mesh pieceMesh;
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("PieceLayer");
        gameObject.tag = "PuzzlePiece";

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<Dragger>();
        pCollider = gameObject.AddComponent<PolygonCollider2D>();
        Rb = gameObject.AddComponent<Rigidbody2D>();
        Rb.isKinematic = true;
    }

    private void Start()
    {
        SetPosition();
        InitializePiece();
    }

    private void InitializePiece()
    {
        //Polygon Collider2D
        pCollider.isTrigger = true;
        pCollider.enabled = false;
        pCollider.pathCount = 1;
        pCollider.SetPath(0, vertices);
        pCollider.enabled = true;

        //Create Mesh
        pieceMesh = pCollider.CreateMesh(false, false);
        gameObject.GetComponent<MeshFilter>().mesh = pieceMesh;

        var material = Resources.Load<Material>("Materials/PieceMaterial" + pieceID.ToString());
        Color materialColor = material.color;
        materialColor.a = (230.0f/255.0f);
        material.color = materialColor;
        gameObject.GetComponent<MeshRenderer>().material = material;
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = "PieceLayer";
        gameObject.GetComponent<MeshRenderer>().sortingOrder = pieceID;

        transform.localScale = new Vector3(sizeOfTile, sizeOfTile);
    }

    private void SetPosition()
    {
        float xRange = Random.Range(-2.5f, 2.5f);
        float yRange = Random.Range(0, 1);
        transform.localPosition = new Vector3(xRange, yRange, -0.1f * pieceID);
    }

    public void SetOrder(int Id, int maxPiece)   // Id of piece that dragged
    {
        if      (pieceID < Id)
            return;
        else if (pieceID > Id)
            pieceID -= 1;
        else if (pieceID == Id)
            pieceID = maxPiece - 1;

        Vector3 pos = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.1f * pieceID);
        transform.localPosition = pos;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = pieceID;
    }
}
