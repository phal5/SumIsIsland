using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 0.5f;
    [SerializeField] bool _reflect;
    [SerializeField] bool _physical;
    [SerializeField] float _resistance;
    
    float acc = 0.001f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 flow = FlowManager.Flow();
        rb.velocity = (rb.velocity - flow) * (1 - FlowManager.Resistance()) + flow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_reflect && other.gameObject.TryGetComponent(out Wall wall))
        {
            rb.velocity = Vector3.Scale(rb.velocity, wall._reflection);
        }
    }
}
