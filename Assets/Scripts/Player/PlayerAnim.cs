using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Transform _launcher;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] SpriteRenderer _renderer;

    private void Update()
    {
        _renderer.sprite = _sprites[GetIndex()];
    }

    int GetIndex()
    {
        int index = (int)((_launcher.eulerAngles.y + 22.5f) / 45f);
        if (index == 8) index = 0;
        return index;
    }
}
