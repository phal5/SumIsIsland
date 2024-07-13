using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] bool _reflect;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = FlowManager.Flow();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Harpoon"))
        {
            _reflect = true;
        }
    }
}
