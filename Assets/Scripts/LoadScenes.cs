using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
