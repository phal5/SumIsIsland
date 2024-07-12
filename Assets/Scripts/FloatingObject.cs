using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float speed = 0.5f;
    public bool isConnected = false;
    private int isPulledBy = 0;
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
        if (!isConnected && isPulledBy == 0)
        {
            transform.Translate(transform.forward * (-1f) * speed);
        }
        else if(isPulledBy == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Island1Pos.position, Time.deltaTime * 100);
        }
        else if(isPulledBy == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, Island2Pos.position, Time.deltaTime * 100);
        }

        if(Vector3.forward != transform.forward)
        {
            if(speed > 0f) { speed -= acc; }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Harpoon1" && !isConnected) // �ۻ� 1�� ���� ���
        {
            isPulledBy = 1;
        }
        else if (other.tag == "Harpoon2" && !isConnected) // �ۻ� 2�� ���� ���
        {
            isPulledBy = 2;
        }
        else if(other.tag == "Island1" &&  isPulledBy == 1) // �ۻ� 1�� �������� ��1�� ���� ���
        {
            transform.parent = other.transform;
            transform.gameObject.tag = "Island1";
            isConnected = true;
            isPulledBy = 0;
        }
        else if(other.tag == "Island2" && isPulledBy == 2) // �ۻ� 2�� �������� ��2�� ���� ���
        {
            transform.parent = other.transform;
            transform.gameObject.tag = "Island2";
            isConnected = true;
            isPulledBy = 0;
        }
        else if(other.tag == "Island1" || other.tag == "Island2") // ��1,2�� ƨ�� ���
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
    }
}
