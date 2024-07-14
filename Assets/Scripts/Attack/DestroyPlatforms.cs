using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DestroyPlatforms : MonoBehaviour
{
    [SerializeField] float _force;
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

            if(collider.transform == IslandManager.Island1()) ApplyDamage(1);
            if(collider.transform == IslandManager.Island2()) ApplyDamage(2);
        }
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

    virtual protected void ApplyDamage(int island) { }
}
