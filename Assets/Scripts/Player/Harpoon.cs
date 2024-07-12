using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] Transform _launcher;
    [SerializeField] Rigidbody _headRigidbody;
    [Space(10f)]
    [SerializeField] float _coolDown;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _launchSpeed;
    [Space(30f)]
    [SerializeField] KeyCode _rotateCW;
    [SerializeField] KeyCode _rotateCCW;
    [SerializeField] KeyCode _launch;

    Vector3 _prevHeadPos;
    bool _cool;
    float _timer;

    // Update is called once per frame
    void Update()
    {
        CoolDown();
        Rotate();
        Launch();
    }

    void CoolDown()
    {
        if (!_cool)
        {
            _timer -= Time.deltaTime;
            if(_timer < 0)
            {
                _cool = true;
            }
        }
    }

    void Launch()
    {
        if (_cool && Input.GetKey(_launch))
        {
            _headRigidbody.transform.position = _launcher.position;
            _headRigidbody.transform.forward = _launcher.forward;
            _headRigidbody.transform.LookAt(_headRigidbody.transform.position + transform.forward);
            _headRigidbody.velocity = transform.forward * _launchSpeed;
            _timer = _coolDown;
            _cool = false;
        }
    }

    void Rotate()
    {
        if (Input.GetKey(_rotateCCW))
        {
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(_rotateCW))
        {
            transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);
        }
    }
}
