using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    [SerializeField] DestroyPlatforms _explosive;
    [SerializeField] float _timer = 5f;

    bool _startTicking = false;

    // Update is called once per frame
    void Update()
    {
        if (_startTicking)
        {
            _timer -= Time.deltaTime;
            if(_timer < 0)
            {
                _explosive.DestroyPlatformsInsideRadius();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Island"))
        {
            _explosive.DestroyPlatformsInsideRadius();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Harpoon"))
        {
            _startTicking = true;
        }
    }
}