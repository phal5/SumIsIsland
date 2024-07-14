using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public float HP_1P = 1.0f;
    public float HP_2P = 1.0f;
    public float timeLimit = 150f;

    float bombDamage = 0.4f;
    float sharkDamage = 0.2f;

    public static float worldTimer = 0f;

    public TextMeshProUGUI timer_text;

    static ScoreKeeper _instance;

    GameManager myGameManager;

    void Start()
    {
        _instance = this;
        myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        worldTimer += Time.deltaTime;
        float time_left = Mathf.Round(((timeLimit - worldTimer) * 100f) / 100f);
        timer_text.text = time_left.ToString();
        checkGameOver();
    }

    public static void HitByBomb(int player_index)
    {
        if(player_index == 1)
        {
            _instance.HP_1P -= _instance.bombDamage;
        }
        else
        {
            _instance.HP_2P -= _instance.bombDamage;
        }
        
    }

    public static void HitByShark(int player_index)
    {
        if (player_index == 1)
        {
            _instance.HP_1P -= _instance.sharkDamage;
        }
        else
        {
            _instance.HP_2P -= _instance.sharkDamage;
        }
    }

    private static void checkGameOver()
    {
        if(_instance.HP_1P < 0)
        {
            _instance.myGameManager.winner_index = 2;
            SceneManager.LoadScene("Ending");
        }
        else if(_instance.HP_2P < 0)
        {
            _instance.myGameManager.winner_index = 1;
            SceneManager.LoadScene("Ending");
        }
        else if(worldTimer >= _instance.timeLimit)
        {
            if(_instance.HP_1P > _instance.HP_2P)
            {
                _instance.myGameManager.winner_index = 1;
            }
            else
            {
                _instance.myGameManager.winner_index = 2;
            }
            SceneManager.LoadScene("Ending");
        }
    }
}
