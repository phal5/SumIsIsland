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
        if (other.tag == "Harpoon1" && !isConnected) // 작살 1에 맞은 경우
        {
            isPulledBy = 1;
        }
        else if (other.tag == "Harpoon2" && !isConnected) // 작살 2에 맞은 경우
        {
            isPulledBy = 2;
        }
        else if(other.tag == "Island1" &&  isPulledBy == 1) // 작살 1에 끌어당겨져 섬1에 붙은 경우
        {
            transform.parent = other.transform;
            transform.gameObject.tag = "Island1";
            isConnected = true;
            isPulledBy = 0;
        }
        else if(other.tag == "Island2" && isPulledBy == 2) // 작살 2에 끌어당겨져 섬2에 붙은 경우
        {
            transform.parent = other.transform;
            transform.gameObject.tag = "Island2";
            isConnected = true;
            isPulledBy = 0;
        }
        else if(other.tag == "Island1" || other.tag == "Island2") // 섬1,2에 튕긴 경우
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
    }
}
