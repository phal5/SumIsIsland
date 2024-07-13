using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public List<Sprite> tutorialSprites = new List<Sprite>();
    int index = 0;

    public Button leftButton;
    public Button rightButton;
    public Button okButton;

    void Start()
    {
        leftButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(false);
    }

    public void TutorialLeftRightButton(bool isLeft)
    {
        if (isLeft)
        {
            if(index > 0)
            {
                index--;
            }
            
        }
        else
        {
            if(index < tutorialSprites.Count - 1)
            {
                index++;
            }
        }
        if (index == 0) //첫번째 슬라이드면
        {
            leftButton.gameObject.SetActive(false);
        }
        else if (index == tutorialSprites.Count - 1) //마지막 슬라이드면
        {
            rightButton.gameObject.SetActive(false);
            okButton.gameObject.SetActive(true);
        }
        else
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            okButton.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = tutorialSprites[index];
    }

    public void TutorialFinished()
    {
        SceneManager.LoadScene("Main");
    }
}
