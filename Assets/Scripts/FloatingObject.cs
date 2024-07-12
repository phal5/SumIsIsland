using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float speed = 0.5f;
    public bool isConnected = false;
    float acc = 0.001f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isConnected)
        {
            transform.Translate(transform.forward * (-1f) * speed);
        }

        if(Vector3.forward != transform.forward)
        {
            if(speed > 0f) { speed -= acc; }
        }
        
    }
}
