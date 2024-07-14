using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    public bool isBGM;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        if (isBGM)
        {
            audioSource.volume = gameManager.BGMVolume;
        }
        else
        {
            audioSource.volume = gameManager.SFXVolume;
        }
        
    }

}
