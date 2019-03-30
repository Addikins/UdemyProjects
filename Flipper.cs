using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    int frames = 0;

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames % 10 == 0)
        {
            FlipXAxis();
        }
    }

    private void FlipXAxis()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        /* xFlip = transform.localScale;
        xFlip.x *= -1;
        transform.localScale = xFlip;
        */
    }
}
