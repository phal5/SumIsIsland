using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Vector3 _reflection = Vector3.one;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FloatObj")
        {
            Destroy(other.gameObject);
        }
    }
}
