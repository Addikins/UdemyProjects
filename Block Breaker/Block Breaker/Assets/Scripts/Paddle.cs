﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float screenWidthInUnits = 16f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float indicatorPosition;
        indicatorPosition = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);   //(Input.mousePosition.x, Input.mousePosition.y);
        paddlePosition.x = Mathf.Clamp(indicatorPosition, minX, maxX);
        transform.position = paddlePosition;
    }
}
