using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSwim : MonoBehaviour
{
    [SerializeField] float _speed = 0.1f;
    [SerializeField] Vector3 _up = new Vector3();
    [SerializeField] Vector3 _halfV1 = Vector3.right + Vector3.forward;
    [SerializeField] Vector3 _halfV2 = Vector3.right - Vector3.forward;
    [SerializeField] float _sinkDepth = 5;

    Vector3 _direction;
    Vector3 _prevPosition;
    float _y = 0;
    float _k = 0;
    bool _flip = false;
    bool _sink = false;

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        SetDirection();
    }

    void SetPosition()
    {
        _k += Time.deltaTime * _speed;
        if (_k > 1)
        {
            _flip ^= true;
            _k -= 1;
        }
        Vector3 pos = _k * (1 - _k) * (_halfV1 + _halfV2) + _k * (1 - _k) * (_k - 0.5f) * (_halfV1 - _halfV2);
        if (_flip) pos.x *= -1;
        transform.position = pos;
    }

    void SetDirection()
    {
        _direction = transform.position - _prevPosition;
        transform.LookAt(transform.position + _direction, _up);
        _prevPosition = transform.position;
    }
    void Sink()
    {

    }
}
