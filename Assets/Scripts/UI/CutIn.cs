using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CutIn : MonoBehaviour
{
    [SerializeField] float _speed;
    [Space(30f)]
    [SerializeField] Transform _begin;
    [SerializeField] Transform _end;
    [Space(30f)]
    [SerializeField] bool _return = false;

    float _moveTimer = 0;
    bool _moving = false;
    bool _forward = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_moving) Animate();
    }

    void Animate()
    {
        Timer();
        float f = (_return) ? 4 * (_moveTimer - _moveTimer * _moveTimer) : (3 - 2 * _moveTimer) * _moveTimer * _moveTimer;
        transform.position = Vector3.Lerp(_begin.position, _end.position, f);
    }

    void Timer()
    {
        _moveTimer += Time.deltaTime * (_forward ? _speed : -_speed);
        if (_moveTimer > 1 || _moveTimer < 0)
        {
            _moving = false;
            _moveTimer = Mathf.Clamp01(_moveTimer);
        }
    }

    public void Play()
    {
        if (!_moving)
        {
            _moving = true;
            _forward ^= true;
        }
    }
}
