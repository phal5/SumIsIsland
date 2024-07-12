using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FloatObj")
        {
            Destroy(other.gameObject);
        }
    }
}
