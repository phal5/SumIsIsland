using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float speed = 0.5f;
    public bool isConnected = false;
    private bool isPulling = false;
    float acc = 0.001f;
    Rigidbody rb;

    Transform Island1Pos;
    Transform Island2Pos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Island1Pos = GameObject.Find("Island1").transform;
        Island2Pos = GameObject.Find("Island2").transform;
    }

    void FixedUpdate()
    {
        if (!isConnected && !isPulling)
        {
            transform.Translate(transform.forward * (-1f) * speed);
        }

        if(Vector3.forward != transform.forward)
        {
            if(speed > 0f) { speed -= acc; }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HarpoonP1")
        {
            Debug.Log("hit by harpoon 1");
            transform.position = Vector3.MoveTowards(transform.position, Island1Pos.position, Time.deltaTime * speed);
        }
        else if (other.tag == "HarpoonP2")
        {
            Debug.Log("hit by harpoon 2");
            transform.position = Vector3.MoveTowards(transform.position, Island2Pos.position, Time.deltaTime * speed);
        }
    }
}
