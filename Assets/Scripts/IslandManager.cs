using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    [SerializeField] Transform _island1;
    [SerializeField] Transform _island2;

    [SerializeField] List<Transform> _platforms1 = new List<Transform>();
    [SerializeField] List<Transform> _platforms2 = new List<Transform>();
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

    public static int Enlist(bool is1, Transform self)
    {
        if (is1)
        {
            _instance._platforms1.Add(self);
            return _instance._platforms1.Count - 1;
        }
        else
        {
            _instance._platforms2.Add(self);
            return _instance._platforms2.Count - 1;
        }
    }

    public static void Remove(bool is1, Transform self)
    {
        if (is1) _instance._platforms1.Remove(self);
        else _instance._platforms2.Remove(self);
    }
    public static Transform GetRandomPlatform()
    {
        int len = _instance._platforms1.Count + _instance._platforms2.Count;
        int id = Random.Range(1, len + 1);
        if(id > _instance._platforms1.Count)
        {
            id -= _instance._platforms1.Count;
            return _instance._platforms2[id - 1];
        }
        else return _instance._platforms1[id - 1];
    }
}
