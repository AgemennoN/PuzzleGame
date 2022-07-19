using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileSensor : MonoBehaviour
{
    private int counter;    // counts the pieces that the sensor is colliding at the moment
    public bool isCovered;  // Checks if only one piece is top of the sensor
    void Start()
    {
        counter = 0;
        isCovered = false;
    }

    private void Update()
    {
        if (counter == 1)
            isCovered = true;
        else
            isCovered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PuzzlePiece") == true)
            counter++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PuzzlePiece") == true)
            counter--;
    }
}
