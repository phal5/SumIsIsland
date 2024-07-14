using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public enum SharkState { DORMANT, ROUNDING, DIVING, ATTACKING};

    [SerializeField] ScoreKeeper _scoreKeeper;
    [SerializeField] GameObject _sharkpedoPrefab;
    [Space (10f)]
    [SerializeField] float _speed = 0.1f;
    [SerializeField] Vector3 _up = new Vector3();
    [SerializeField] Vector3 _halfV1 = Vector3.right + Vector3.forward;
    [SerializeField] Vector3 _halfV2 = Vector3.right - Vector3.forward;
    [SerializeField] float _sinkDepth = 5;

    Transform _target;
    [SerializeField]SharkState _state = SharkState.ROUNDING;
    Vector3 _direction;
    Vector3 _prevPosition;
    float _showDownTimer = 0;
    float _y = 0;
    float _k = 0;
    bool _flip = false;

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case SharkState.ROUNDING:
                {
                    Rise();
                    SetPosition();
                    SetDirection();
                    break;
                }
            case SharkState.DIVING:
                {
                    Sink();
                    SetPosition();
                    SetDirection();
                    break;
                }
            case SharkState.ATTACKING:
                {
                    Attack();
                    break;
                }
            case SharkState.DORMANT:
                {
                    WaitPatiently();
                    break;
                }
        }
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
        pos += Vector3.up * _y;
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
        if(_y >= -_sinkDepth)
        {
            _y -= Time.deltaTime;
            if(_y < -_sinkDepth)
            {
                _y = -_sinkDepth;

                _state = SharkState.ATTACKING;
                _target = IslandManager.GetRandomPlatform();
            }
        }
    }

    void Rise()
    {
        if(_y <= 0)
        {
            _y += Time.deltaTime * 2.4f;
            if(_y > 0)
            {
                _y = 0;
                _state = SharkState.DIVING;
            }
        }
    }

    void Attack()
    {
        if(Instantiate(_sharkpedoPrefab, _target.position - Vector3.up * 5, Quaternion.identity).TryGetComponent(out SharkTorpedo sharkPedo))
            sharkPedo.SetTarget(_target);
        _target = null;

        _state = SharkState.DORMANT;
        _showDownTimer = NextScreenTime();
    }

    void WaitPatiently()
    {
        _showDownTimer -= Time.deltaTime;
        if(_showDownTimer < 0 )
        {
            _state = SharkState.ROUNDING;
        }
    }

    float NextScreenTime()
    {
        float left = _scoreKeeper.timeLimit - ScoreKeeper.worldTimer;
        return (left * 0.3f) * Random.Range(0.9f, 1.1f);
    }
}
