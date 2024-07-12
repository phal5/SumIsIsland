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
            // 충돌 지점과 충돌 물체의 중심을 이용해 법선 벡터 계산
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = (transform.position - contactPoint).normalized;

            // 입사 벡터는 현재 이동 방향
            Vector3 incomingVector = transform.forward;

            // 반사 벡터 계산
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normal);

            // 물체의 이동 방향을 반사 벡터로 설정
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
