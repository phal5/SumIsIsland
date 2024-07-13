using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float BGMVolume;
    public float SFXVolume;

    public int winner_index = 0;
    public bool time_over = false;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
