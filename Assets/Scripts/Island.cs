using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Island : MonoBehaviour
{
    public float spinSpeed = 22.5f;
    [SerializeField] Image _hpBar;
    
    float _hitPoint = 1;

    void Update()
    {
        Animate();
    }

    void Animate()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }

    void Fill()
    {
        //_hpBar.
    }
}
