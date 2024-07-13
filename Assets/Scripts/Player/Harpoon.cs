using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] HarpoonLauncher _launcher;
    [SerializeField] Transform _launcherTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_launcher._state == HarpoonLauncher.HarpoonState.COOL)
        {
            transform.LookAt(transform.position + _launcherTransform.up, Vector3.up);
            transform.position = _launcherTransform.position + _launcher._harpoonOffset * transform.forward - Vector3.up * 0.1f;
        }
    }

    public void Return()
    {
        _launcher.SetReturn();
    }
}
