using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public float spinSpeed = 100;

    void Update()
    {
        if (Input.GetButton("P1_LSpin"))
        {
            transform.Rotate(0, -spinSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetButton("P1_RSpin"))
        {
            transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetButton("P2_LSpin"))
        {
            transform.Rotate(0, -spinSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetButton("P2_RSpin"))
        {
            transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        }
    }
}
