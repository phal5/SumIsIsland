using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    private static FlowManager _instance;
    [SerializeField] Vector3 _flow;
    [SerializeField] float _resistance;

    //Singleton
    private void Awake()
    {
        if(_instance == null) _instance = this;
        else
        {
            Debug.LogError("Multiple Instances of FlowManager has been spotted under " + gameObject.name + "!");
            Destroy(this);
        }
    }

    public static Vector3 Flow()
    {
        return _instance._flow;
    }

    public static float Resistance()
    {
        return _instance._resistance;
    }
}
