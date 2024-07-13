using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DestroyPlatforms : MonoBehaviour
{
    [SerializeField] float _force;
    [SerializeField] float _damage;
    public float _radius;
    [SerializeField] UnityEvent _onDestroy;

    public void DestroyPlatformsInsideRadius()
    {
        foreach (Collider collider in CollidersInsideRadius(_radius))
        {
            if(collider.gameObject.layer == LayerMask.NameToLayer("Island") && collider.gameObject.TryGetComponent(out FloatingObject fO))
            {
                fO.SetConnection(false);
                fO.StartSinking();
                collider.gameObject.tag = "FloatObj";
                collider.enabled = false;
            }
            if (collider.gameObject.TryGetComponent(out Rigidbody rb))
            {
                float divisor = (collider.transform.position - transform.position).sqrMagnitude;
                divisor = (divisor < 0.01f) ? 0.01f : divisor;

                rb.velocity = (collider.transform.position - transform.position).normalized / divisor * _force;
            }
        }
        if ((IslandManager.Island1().position - transform.position).sqrMagnitude < _radius * _radius) ;
        if ((IslandManager.Island2().position - transform.position).sqrMagnitude < _radius * _radius) ;
        _onDestroy.Invoke();
    }

    Collider[] CollidersInsideRadius(float radius)
    {
        return Physics.OverlapSphere(transform.position, radius);
    }

    public void DestroyThis()
    {
        Destroy(this);
    }
}
