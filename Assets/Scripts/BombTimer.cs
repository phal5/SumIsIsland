using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    [SerializeField] DestroyPlatforms _explosive;
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
                _explosive.DestroyPlatformsInsideRadius();
                _collider.enabled = false;
                Destroy(this);
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
            _explosive.DestroyPlatformsInsideRadius();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Harpoon"))
        {
            _startTicking = true;
        }
    }

    public void Destroy()
    {
        _destroyTicking = true;
    }
}
