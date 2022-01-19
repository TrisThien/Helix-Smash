using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.smoothDeltaTime, 0);
    }
}