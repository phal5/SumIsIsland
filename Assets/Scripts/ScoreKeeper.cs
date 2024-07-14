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

    [SerializeField] CutIn _cutIn1;
    [SerializeField] CutIn _cutIn2;

    float bombDamage = 0.4f;
    float sharkDamage = 0.2f;

    public static float worldTimer = 0f;

    public TextMeshProUGUI timer_text;

    static ScoreKeeper _instance;

    GameManager myGameManager;

    public AudioSource KO;

    bool isGameOngoing = false;

    public GameObject eStart;
    public GameObject eFinish;
    public GameObject e1PWin;
    public GameObject e2PWin;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Instantiate(eStart);
        StartCoroutine(StartDelay());
        myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private IEnumerator StartDelay()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        isGameOngoing = true;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (isGameOngoing)
        {
            worldTimer += Time.deltaTime;
            float time_left = Mathf.Round(((timeLimit - worldTimer) * 100f) / 100f);
            timer_text.text = time_left.ToString();
            checkGameOver();
        }
        
    }

    public static void HitByBomb(int player_index)
    {
        if(player_index == 1)
        {
            _instance.HP_1P -= _instance.bombDamage;
            _instance._cutIn2.Play();
        }
        else
        {
            _instance.HP_2P -= _instance.bombDamage;
            _instance._cutIn1.Play();
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

    private void checkGameOver()
    {
        if(_instance.HP_1P < 0)
        {
            _instance.myGameManager.winner_index = 2;
            StartCoroutine(GoToEnding());
        }
        else if(_instance.HP_2P < 0)
        {
            _instance.myGameManager.winner_index = 1;
            StartCoroutine(GoToEnding());
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
            StartCoroutine(GoToEnding());
        }
    }

    private IEnumerator GoToEnding()
    {
        KO.Play();
        Instantiate(eFinish);
        if (_instance.myGameManager.winner_index == 1) { Instantiate(e1PWin); }
        else if (_instance.myGameManager.winner_index == 2) { Instantiate(e2PWin); }
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Ending");

    }
}
