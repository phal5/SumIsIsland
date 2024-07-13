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
    public float timeLimit = 120f;

    float bombDamage = 0.4f;
    float sharkDamage = 0.2f;

    public static float worldTimer = 0f;

    public TextMeshProUGUI timer_text;

    private void Update()
    {
        worldTimer += Time.deltaTime;
        timer_text.text = (timeLimit - worldTimer).ToString();
    }

    public void HitByBomb(int player_index)
    {
        if(player_index == 1)
        {
            HP_1P -= bombDamage;
        }
        else
        {
            HP_2P -= bombDamage;
        }
    }

    public void HitByShark(int player_index)
    {
        if (player_index == 1)
        {
            HP_1P -= sharkDamage;
        }
        else
        {
            HP_2P -= sharkDamage;
        }
    }

    private void checkGameOver()
    {
        if(HP_1P < 0)
        {
            SceneManager.LoadScene("Ending");
        }
        else if(HP_2P < 0)
        {
            SceneManager.LoadScene("Ending");
        }
        else if(worldTimer >= timeLimit)
        {
            SceneManager.LoadScene("Ending");
        }
    }
}
