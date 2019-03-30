using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float minRotationSpeed = -720f;
    [SerializeField] float maxRotationSpeed = 720f;
    float rotationSpeed;

    void Start()
    {
        int rotationChoice = Random.Range(0, 1);
        switch (rotationSpeed)
        {
            case 0:
                rotationSpeed = minRotationSpeed;
                break;
            case 2:
                rotationSpeed = maxRotationSpeed;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
