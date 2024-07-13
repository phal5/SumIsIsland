using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] Transform _from;
    [SerializeField] Transform _to;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one - Vector3.forward + (_to.position - _from.position).magnitude * Vector3.forward;
        transform.position = _from.position - Vector3.up * 0.55f;
        transform.LookAt(_to.position - Vector3.up * 0.55f);
    }
}
