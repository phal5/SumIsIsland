using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Harpoon : MonoBehaviour
{
    [SerializeField] Transform _launcher;
    [SerializeField] Rigidbody _headRigidbody;
    [Space(10f)]
    [SerializeField] float _coolDown;
    [SerializeField] float _returnAfter;
    [SerializeField] float _returnSpeed;
    [Space(10f)]
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _launchSpeed;
    [Space(30f)]
    [SerializeField] KeyCode _rotateCW;
    [SerializeField] KeyCode _rotateCCW;
    [SerializeField] KeyCode _launch;

    bool _cool;
    bool _return;
    float _coolDownTimer;
    float _returnTimer;

    // Update is called once per frame
    void Update()
    {
        CoolDown();
        Rotate();
        Launch();
        if (!_return) ReturnTimer();
    }

    private void FixedUpdate()
    {
        Vector3 flow = FlowManager.Flow() + _launcher.transform.position - _headRigidbody.transform.position;
        _headRigidbody.velocity = (_headRigidbody.velocity - flow) * (1 - FlowManager.Resistance()) + flow;
    }

    void CoolDown()
    {
        if (!_cool)
        {
            _coolDownTimer -= Time.deltaTime;
            if(_coolDownTimer < 0)
            {
                _cool = true;
            }
        }
    }

    void Launch()
    {
        if (_cool && Input.GetKeyDown(_launch))
        {
            _headRigidbody.transform.position = _launcher.position;
            _headRigidbody.transform.LookAt(_headRigidbody.transform.position + transform.forward, Vector3.up);

            _headRigidbody.velocity = transform.forward * _launchSpeed;
            _headRigidbody.angularVelocity = Vector3.zero;

            _coolDownTimer = _coolDown;
            _cool = false;
            
            _returnTimer = _returnAfter;
            _return = false;
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
        _launcher.position = Vector3.Scale(_launcher.position, Vector3.one + Vector3.down);
        _launcher.position += (transform.forward.z * -0.01f + transform.position.y) * Vector3.up;
        SetLayerOfDirectChildren(_headRigidbody.transform, "Harpoon");
    }

    void ReturnTimer()
    {
        SetLayerOfDirectChildren(_headRigidbody.transform, "NonColliding");
    }

    void SetLayerOfDirectChildren(Transform parent, string layerName)
    {
        foreach(Transform child in parent)
        {
            child.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }

    public void SetReturn()
    {
        _return = true;
    }
}
