using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] bool _destroyOnTouch = true;
    public Vector3 _reflection = Vector3.one;

    private void Awake()
    {
        if(TryGetComponent(out MeshRenderer renderer)) renderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_destroyOnTouch && other.tag == "FloatObj")
        {
            Destroy(other.gameObject);
        }
    }
}
