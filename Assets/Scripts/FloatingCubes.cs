using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCubes : MonoBehaviour
{
    public float floatExtent;
    public float floatSpeed;
    private float tempVal;
    private Vector3 tempPos;

    void Start()
    {
        tempVal = transform.position.y;
        tempPos = transform.position;
    }

    void Update()
    {
        tempPos.y = tempVal + floatExtent * Mathf.Sin(floatSpeed * Time.time);
        transform.position = tempPos;
    }
}
