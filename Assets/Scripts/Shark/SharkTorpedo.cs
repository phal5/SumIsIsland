using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkTorpedo : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] Collider _collider;
    [SerializeField] DestroyPlatforms _sharkDestroy;
    [SerializeField] GameObject _shark;

    Transform _target;
    float _destroyAfter = 1.083f;
    float _attackAfter = 0.52f;
    bool _tickDestruction = false;
    bool _tickAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody.velocity = Vector3.up * 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_tickDestruction) _rigidBody.velocity = (_target.position - transform.position).normalized * 20;
        else
        {
            transform.rotation = Quaternion.identity;

            if (_tickAttack)
            {
                _attackAfter -= Time.deltaTime;
                if (_attackAfter < 0)
                {
                    _sharkDestroy.DestroyPlatformsInsideRadius();
                }
            }
            _destroyAfter -= Time.deltaTime;
            if(_destroyAfter < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == _target)
        {
            transform.parent = _target;
            transform.localPosition = Vector3.zero;
            Destroy(_rigidBody);
            _shark.SetActive(true);
            _tickDestruction = true;
            _tickAttack = true;
        }
    }
}
