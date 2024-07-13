using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    [SerializeField] DestroyPlatforms _explosive;
    [SerializeField] float _radius = 15;
    [Space(10f)]
    [SerializeField] Collider _collider;
    [SerializeField] float _timer = 5f;

    bool _startTicking = false;
    bool _destroyTicking = false;
    float _destroyTimer = 1;

    // Update is called once per frame
    void Update()
    {
        if (_startTicking)
        {
            _timer -= Time.deltaTime;
            if(_timer < 0)
            {
                SetOff();
                _collider.enabled = false;
                _startTicking = false;
            }
        }
        else if (_destroyTicking)
        {
            _destroyTimer -= Time.deltaTime;
            if(_destroyTimer < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Island") && _startTicking)
        {
            SetOff();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Harpoon"))
        {
            _startTicking = true;
        }
    }

    void SetOff()
    {
        _explosive.DestroyPlatformsInsideRadius(_radius);
        foreach (Collider collider in Physics.OverlapSphere(transform.position, _radius))
        {
            if (collider.gameObject.TryGetComponent(out Bomb bomb))
            {
                bomb.Activate();
            }
            if (collider.gameObject.TryGetComponent(out BombTimer timer))
            {
                timer._startTicking = true;
            }
        }
    }

    public void Destroy()
    {
        _destroyTicking = true;
    }
}
