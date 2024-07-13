using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HarpoonLauncher : MonoBehaviour
{
    public enum HarpoonState {COOL, LAUNCHED, RETURN};

    [SerializeField] Transform _launcher;
    [SerializeField] Rigidbody _headRigidbody;
    [Space(10f)]
    [SerializeField] float _returnAfter;
    [SerializeField] float _returnSpeed;
    [Space(10f)]
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _launchSpeed;
    [Space(30f)]
    [SerializeField] KeyCode _rotateCW;
    [SerializeField] KeyCode _rotateCCW;
    [SerializeField] KeyCode _launch;

    public float _harpoonOffset { get; private set; }
    public HarpoonState _state { get; private set; }
    float _returnTimer;

    //Trivial
    float _headRotateTimer;

    private void Start()
    {
        SetLayerOfDirectChildren(_headRigidbody.transform, "NonColliding");
        _harpoonOffset = (_launcher.position - _headRigidbody.transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        switch (_state)
        {
            case HarpoonState.COOL:
                if (Input.GetKeyDown(_launch)) Launch();
                break;
            case HarpoonState.LAUNCHED:
                if(_headRigidbody.isKinematic == false) ReturnTimer();
                else SetLayerOfDirectChildren(_headRigidbody.transform, "NonColliding");
                break;
        }
    }

    private void FixedUpdate()
    {
        if (_state == HarpoonState.RETURN) Return();
    }

    //Fire & Forget
    void Launch()
    {
        SetLayerOfDirectChildren(_headRigidbody.transform, "Harpoon");

        _headRigidbody.transform.SetParent(transform.parent);
        _headRigidbody.transform.LookAt(_headRigidbody.transform.position + transform.forward, Vector3.up);

        _headRigidbody.velocity = transform.forward * _launchSpeed;
        _headRigidbody.angularVelocity = Vector3.zero;

        _state = HarpoonState.LAUNCHED;
        _returnTimer = _returnAfter;
    }

    void Rotate()
    {
        if (Input.GetKey(_rotateCCW))
        {
            transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(_rotateCW))
        {
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
        _launcher.position = Vector3.Scale(_launcher.position, Vector3.one + Vector3.down);
        _launcher.position += (transform.forward.z * -0.01f + transform.position.y) * Vector3.up;
    }

    void ReturnTimer()
    {
        _returnTimer -= Time.deltaTime;
        if (_returnTimer < 0)
        {
            SetReturn();
        }
    }

    void Return()
    {
        Vector3 flow = (_launcher.position + _launcher.transform.up * _harpoonOffset - _headRigidbody.transform.position).normalized * _returnSpeed;
        _headRigidbody.velocity = (_headRigidbody.velocity - flow) * 0.95f + flow;

        float proximity = (_headRigidbody.transform.position - _launcher.position - _launcher.transform.up * _harpoonOffset).sqrMagnitude;

        _headRigidbody.transform.rotation =
            Quaternion.Lerp(Quaternion.LookRotation(-_headRigidbody.velocity, Vector3.up), _headRigidbody.transform.rotation, (proximity > 100) ? 0.95f : proximity * 0.01f);

        if(proximity < 1)
        {
            _headRigidbody.velocity = Vector3.zero;
            _headRigidbody.angularVelocity = Vector3.zero;
            _state = HarpoonState.COOL;
        }
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
        SetLayerOfDirectChildren(_headRigidbody.transform, "NonColliding");
        _state = HarpoonState.RETURN;
        _headRotateTimer = 0;
    }
}