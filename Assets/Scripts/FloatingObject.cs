using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public bool isConnected = false;
    bool _onceConnected = false;

    int isPulledBy = 0;
    Rigidbody rb;
    Transform Island1Pos;
    Transform Island2Pos;

    Rigidbody _harpoon;
    Transform _harpoonParent;

    bool _sink;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = FlowManager.Flow();
        Island1Pos = IslandManager.Island1().transform;
        Island2Pos = IslandManager.Island2().transform;
    }

    private void Update()
    {
        if (_sink)
        {
            transform.position += Vector3.down * Time.deltaTime * 10;
        }
    }

    void FixedUpdate()
    {
        if (!isConnected)
        {
            switch (isPulledBy)
            {
                case 0:
                    {
                        rb.velocity *= 1 - FlowManager.Resistance();
                        Vector3 flow = FlowManager.Flow();
                        rb.velocity -= Vector3.Dot(flow.normalized, rb.velocity - flow) * flow.normalized * (1 - FlowManager.Resistance());
                        break;
                    }
                case 1:
                    {
                        Vector3 pull = (Island1Pos.position - transform.position).normalized * 200;
                        rb.velocity = (rb.velocity - pull) * 0.95f + pull;
                    }
                    break;
                case 2:
                    {
                        Vector3 pull = (Island2Pos.position - transform.position).normalized * 200;
                        rb.velocity = (rb.velocity - pull) * 0.95f + pull;
                        break;
                    }
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Harpoon1" && isPulledBy == 0 && !_onceConnected) // 작살 1에 맞은 경우
        {
            isPulledBy = 1;
            _harpoon = other.rigidbody;
            gameObject.tag = "Untagged";
            Fix(_harpoon);
        }
        else if (other.collider.tag == "Harpoon2" && isPulledBy == 0 && !_onceConnected) // 작살 2에 맞은 경우
        {
            isPulledBy = 2;
            _harpoon = other.rigidbody;
            gameObject.tag = "Untagged";
            Fix(_harpoon);
        }
        else if(other.collider.tag == "Island1" &&  isPulledBy == 1) // 작살 1에 끌어당겨져 섬1에 붙은 경우
        {
            transform.parent = other.transform;
            transform.gameObject.tag = "Island1";
            Loosen(_harpoon);
            SetConnection(true);
            isPulledBy = 0;
            _onceConnected = true;
        }
        else if(other.collider.tag == "Island2" && isPulledBy == 2) // 작살 2에 끌어당겨져 섬2에 붙은 경우
        {
            transform.parent = other.transform;
            transform.gameObject.tag = "Island2";
            Loosen(_harpoon);
            SetConnection(true);
            isPulledBy = 0;
            _onceConnected = true;
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
        rb.isKinematic = false;
        isConnected = connected;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.isKinematic = connected;
        gameObject.layer = LayerMask.NameToLayer(connected ? "Island" : "Default");

        if (!connected) transform.parent = null;
    }

    void Fix(Rigidbody rigidBody)
    {
        _harpoonParent = rigidBody.transform.parent;
        rigidBody.transform.parent = transform;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;

        rb.centerOfMass = rigidBody.transform.localPosition;
    }

    void Loosen(Rigidbody rigidBody)
    {
        rigidBody.transform.parent = _harpoonParent;
        rigidBody.isKinematic = false;
        if (rigidBody.TryGetComponent(out Harpoon harpoon)) harpoon.Return();

        rb.centerOfMass = Vector3.zero;
    }

    public void StartSinking()
    {
        _sink = true;
    }
}
