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
        rb.velocity *= 1 - FlowManager.Resistance();

        Vector3 flow = FlowManager.Flow();
        rb.velocity -= Vector3.Dot(flow.normalized, rb.velocity - flow) * flow.normalized * (1 - FlowManager.Resistance());
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Wall")
        {
            Physics.Raycast(transform.position, rb.velocity, out RaycastHit hit);
            if(hit.collider == other.collider)
            {
                rb.velocity += Vector3.Dot(hit.normal, rb.velocity) * hit.normal * 2;
            }
            else
            {
                rb.velocity *= -1;
            }

        }
        else if (other.collider.tag == "Harpoon1" || other.collider.tag == "Harpoon2")
        {
            
        }
        else
        {

        }
    }
}
