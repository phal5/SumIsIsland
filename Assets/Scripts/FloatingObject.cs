using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        transform.Translate(Vector3.forward * (-1f) * speed);
    }
}
