using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] HarpoonLauncher _launcher;
    [SerializeField] Transform _launcherTransform;

    AudioSource arrowhit;

    // Start is called before the first frame update
    void Start()
    {
        arrowhit = GameObject.Find("arrowhit").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_launcher._state == HarpoonLauncher.HarpoonState.COOL)
        {
            transform.LookAt(transform.position + _launcherTransform.up, Vector3.up);
            transform.position = _launcherTransform.position + _launcher._harpoonOffset * transform.forward - Vector3.up * 0.01f;
        }
        else
        {
            RetainHeight();
        }
    }

    public void Return()
    {
        _launcher.SetReturn();
    }

    private void OnCollisionEnter(Collision collision)
    {
        arrowhit.Play();
    }

    void RetainHeight()
    {
        transform.position += Vector3.Scale(_launcherTransform.position - transform.position, Vector3.up) - Vector3.up * 0.01f;
    }
}
