using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
    }

    void Update()
    {
       transform.position = new Vector3(target.position.x, 0, target.position.z) + initialPosition;
    }
}
