using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public bool isCovered; 
    private GameObject[] Sensors;
    void Start()
    {
        isCovered = false;
        Sensors = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Sensors[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        isCovered = checkSensors();
    }

    // If all the sensors on the tile is covered that means the tile is also covered
    // If all the GridTiles are covered level ends
    private bool checkSensors()
    {
        bool result = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(Sensors[i].GetComponent<GridTileSensor>().isCovered == false)
            {
                result = false;
            }
        }
        return result;
    }
}
