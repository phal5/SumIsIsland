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

    public HarpoonState _state { get; private set; }
    float _returnTimer;

    private void Start()
    {
        SetLayerOfDirectChildren(_headRigidbody.transform, "NonColliding");
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
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(_rotateCW))
        {
            transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);
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
        Vector3 flow = (_launcher.transform.position - _headRigidbody.transform.position).normalized * _returnSpeed;

        _headRigidbody.velocity = (_headRigidbody.velocity - flow) * 0.95f + flow;
        _headRigidbody.transform.rotation =
            Quaternion.Lerp(Quaternion.LookRotation(-_headRigidbody.velocity, Vector3.up), _headRigidbody.transform.rotation, Time.fixedDeltaTime);

        if((_headRigidbody.transform.position - _launcher.position).sqrMagnitude < 1)
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
    }
}
