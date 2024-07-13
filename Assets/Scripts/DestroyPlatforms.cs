using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DestroyPlatforms : MonoBehaviour
{
    [SerializeField] float _radius;
    [SerializeField] float _force;
    [SerializeField] UnityEvent _onDestroy;

    public void DestroyPlatformsInsideRadius()
    {
        foreach (Collider collider in CollidersInsideRadius())
        {
            if(collider.gameObject.layer == LayerMask.NameToLayer("Island") && collider.gameObject.TryGetComponent(out FloatingObject fO))
            {
                fO.SetConnection(false);
                fO.StartSinking();
                collider.gameObject.tag = "FloatObj";
                collider.enabled = false;
                if(collider.gameObject.TryGetComponent(out Rigidbody rb))
                {
                    float divisor = (collider.transform.position - transform.position).sqrMagnitude;
                    divisor = (divisor < 0.01f) ? 0.01f : divisor;

                    rb.velocity = (collider.transform.position - transform.position).normalized / divisor * _force;
                }
            }
        }
        _onDestroy.Invoke();
    }

    Collider[] CollidersInsideRadius()
    {
        return Physics.OverlapSphere(transform.position, _radius);
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
