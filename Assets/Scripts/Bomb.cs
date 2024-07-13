using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] bool _reflect;

    Rigidbody rb;

    AudioSource fuse_burn;
    AudioSource bombhit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = FlowManager.Flow();

        fuse_burn = GameObject.Find("fuse_burn").GetComponent<AudioSource>();
        bombhit = GameObject.Find("bombhit").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Vector3 flow = FlowManager.Flow();
        rb.velocity = (rb.velocity - flow) * (1 - FlowManager.Resistance()) + flow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_reflect && other.gameObject.TryGetComponent(out Wall wall))
        {
            rb.velocity = Vector3.Scale(rb.velocity, wall._reflection);
        }
        bombhit.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Harpoon"))
        {
            Activate();
            collision.gameObject.GetComponent<Harpoon>().Return();
        }
        bombhit.Play();
        fuse_burn.Stop();
    }

    public void Activate()
    {
        _reflect = true;
        gameObject.tag = "Untagged";
        fuse_burn.Play();
    }
}
