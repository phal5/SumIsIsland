using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject Winner_1P;
    public GameObject Winner_2P;
    public GameObject Vid_1P;
    public GameObject Vid_2P;
    void Start()
    {
        GameManager myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(myGameManager.winner_index == 1)
        {
            Winner_1P.SetActive(true);
            Vid_1P.SetActive(true);
        }
        else if (myGameManager.winner_index == 2)
        {
            Winner_2P.SetActive(true);
            Vid_2P.SetActive(true);
        }
        
    }

}
