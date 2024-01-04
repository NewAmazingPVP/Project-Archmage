using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpinScript : MonoBehaviour
{
    public float rotationSpeed = 30f;
    
    public Vector3 rotationAxis = Vector3.up;
    
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}