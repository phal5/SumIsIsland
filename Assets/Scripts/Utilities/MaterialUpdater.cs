using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialUpdater : MonoBehaviour
{
    [SerializeField] Material[] _materials;
    [SerializeField] Texture[] _textures;
    [Space(30f)]
    [SerializeField] bool _update;

    // Update is called once per frame
    void Update()
    {
        if(_update)
        {
            _update = false;
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetTexture("_BaseMap", _textures[i]);
            }
        }
    }
}
