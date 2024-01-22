using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardCamera : MonoBehaviour
{
    public Vector3 positionMin;
    public Vector3 positionMax;
    public float scrollSpeed = 1.0f;

    void Start()
    {
        transform.position = positionMin;
    }


    void Update()
    {

        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
            Camera.main.transform.position -= new Vector3(0, 1, 1) * Input.GetAxisRaw("Mouse ScrollWheel") * scrollSpeed;
        }
    }
}
