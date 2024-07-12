using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 0.5f;
    float acc = 0.001f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    void FixedUpdate()
    {
        transform.Translate(transform.forward * (-1f) * speed);
        /*
        if (Vector3.forward != transform.forward)
        {
            if (speed > 0.1f) { speed -= acc; }
        }
        */
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            // �浹 ������ �浹 ��ü�� �߽��� �̿��� ���� ���� ���
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = (transform.position - contactPoint).normalized;

            // �Ի� ���ʹ� ���� �̵� ����
            Vector3 incomingVector = transform.forward;

            // �ݻ� ���� ���
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normal);

            // ��ü�� �̵� ������ �ݻ� ���ͷ� ����
            transform.forward = reflectVector;

        }
        else if (other.tag == "Harpoon1" || other.tag == "Harpoon2")
        {
            transform.LookAt(other.transform.forward);
        }
        else
        {

        }
        /*
        else if(other.tag == "Island1" || other.tag == "Island2")
        {
            //Destroy(other.transform.parent.gameObject);
            Destroy(other);
            Destroy(this);
        }
        */
    }
}
