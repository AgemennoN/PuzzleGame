using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSquare : MonoBehaviour
{
    private float speed;
    private float rotatingSpeed;
    private float scaling;
    private int materialId;
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        speed = Random.Range(0.5f, 2.5f);
        rotatingSpeed = Random.Range(-1.5f, 1.5f);

        scaling = Random.Range(0.5f, 2.5f);
        transform.localScale = transform.localScale * scaling;

        materialId = Random.Range(0, 11);
        var material = Resources.Load<Material>("Materials/PieceMaterial" + materialId.ToString());
        gameObject.GetComponent<Renderer>().material = material;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotatingSpeed, Space.Self);

        if (transform.position.y > 8)
        {
            Object.Destroy(gameObject);
        }
    }


}
