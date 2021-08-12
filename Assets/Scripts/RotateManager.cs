using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{

    public float turnSpeed;
    public Vector3 axis;

    void Update()
    {
        transform.Rotate(axis, Time.deltaTime * turnSpeed,Space.Self);
    }
}
