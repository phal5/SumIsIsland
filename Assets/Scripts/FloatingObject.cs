using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public bool isConnected = false;

    int isPulledBy = 0;
    Rigidbody rb;
    Transform Island1Pos;
    Transform Island2Pos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = FlowManager.Flow();
        Island1Pos = GameObject.Find("Island1").transform;
        Island2Pos = GameObject.Find("Island2").transform;
    }

    void FixedUpdate()
    {
        if (!isConnected && isPulledBy == 0)
        {
            rb.velocity *= 1 - FlowManager.Resistance();

            Vector3 flow = FlowManager.Flow();
            rb.velocity -= Vector3.Dot(flow.normalized, rb.velocity - flow) * flow.normalized * (1 - FlowManager.Resistance());
        }
        else if(isPulledBy == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Island1Pos.position, Time.deltaTime * 100);
        }
        else if(isPulledBy == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, Island2Pos.position, Time.deltaTime * 100);
        }        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Harpoon1" && !isConnected) // 작살 1에 맞은 경우
        {
            isPulledBy = 1;
            Debug.Log("Hit");
        }
        else if (other.collider.tag == "Harpoon2" && !isConnected) // 작살 2에 맞은 경우
        {
            isPulledBy = 2;
        }
        else if(other.collider.tag == "Island1" &&  isPulledBy == 1) // 작살 1에 끌어당겨져 섬1에 붙은 경우
        {
            transform.parent = other.transform;
            transform.gameObject.tag = "Island1";
            SetConnection(true);
            isPulledBy = 0;
        }
        else if(other.collider.tag == "Island2" && isPulledBy == 2) // 작살 2에 끌어당겨져 섬2에 붙은 경우
        {
            transform.parent = other.transform;
            transform.gameObject.tag = "Island2";
            SetConnection(true);
            isPulledBy = 0;
        }
        else if(other.collider.tag == "Island1" || other.collider.tag == "Island2") // 섬1,2에 튕긴 경우
        {
            Physics.Raycast(transform.position, rb.velocity, out RaycastHit hit);
            if (hit.collider == other.collider)
            {
                rb.velocity += Vector3.Dot(hit.normal, rb.velocity) * hit.normal * 2;
            }
            else
            {
                rb.velocity *= -1;
            }
        }
    }

    private void OnDestroy()
    {
        SetConnection(false);
        Destroy(gameObject);
    }

    public void SetConnection(bool connected)
    {
        isConnected = connected;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.isKinematic = connected;

        gameObject.layer = LayerMask.NameToLayer(connected ? "Island" : "Default");
    }
}
