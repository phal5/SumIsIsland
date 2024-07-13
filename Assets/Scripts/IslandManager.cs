using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    [SerializeField] Transform _island1;
    [SerializeField] Transform _island2;

    static IslandManager _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }

    public static Transform Island1()
    {
        return _instance._island1;
    }

    public static Transform Island2()
    {
        return _instance._island2;
    }
}
