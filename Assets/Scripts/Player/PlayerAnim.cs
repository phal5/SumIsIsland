using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public KeyCode myLeft;
    public KeyCode myRight;
    public int PlayerIndex;
    
    SpriteRenderer mySpriteRenderer;

    public List<Sprite> mySprites = new List<Sprite>();
    float angle;

    Animator myAnimator;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        Transform HarpoonHead = transform.parent.transform.Find("HarpoonAxis").transform;
        Quaternion myRotation = HarpoonHead.rotation;
        angle = myRotation.y;
    }

    void Update()
    {
        if (Input.GetKey(myLeft)) //���� ��ǲ�� ��
        {
            myAnimator.SetBool("isWalk", true);
            // ���� ���ʿ� ������ �������� �ȱ�
            if (angle >= 0 && angle <= 180 && PlayerIndex == 2) //p2 �������� ���ֱ�
            {
                mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            }

            // ���� ���ʿ� ������ ���������� �ȱ�
            if ((angle > 180 || angle < 0) && PlayerIndex == 1) //p1 ���������� ���ֱ�
            {
                mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            }

        }
        else if(Input.GetKey(myRight)) //������ ��ǲ�� ��
        {
            myAnimator.SetBool("isWalk", true);
            // ���� ���ʿ� ������ ���������� �ȱ�
            if (angle >= 0 && angle <= 180 && PlayerIndex == 1) //p1 ���������� ���ֱ�
            {
                mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            }

            // ���� ���ʿ� ������ �������� �ȱ�
            if ((angle > 180 || angle < 0) && PlayerIndex == 2) //p2 �������� ���ֱ�
            {
                mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            }

        }
        else
        {
            myAnimator.SetBool("isWalk", false);
            checkPlayerDirection();
        }
    }

    private void checkPlayerDirection()
    {
        if(angle >= -22.5 && angle < 22.5) // L
        {
            mySpriteRenderer.sprite = mySprites[0];
        }
        else if(angle >= 22.5 && angle < 67.5) // BL
        {
            mySpriteRenderer.sprite = mySprites[1];
        }
        else if(angle >= 67.5 && angle < 112.5) // B
        {
            mySpriteRenderer.sprite = mySprites[2];
        }
        else if(angle >= 112.5 && angle < 157.5) // BR
        {
            mySpriteRenderer.sprite = mySprites[3];
        }
        else if(angle >= 157.5 && angle < 202.5) // R
        {
            mySpriteRenderer.sprite = mySprites[4];
        }
        else if(angle >= 202.5 && angle < 247.5) // FR
        {
            mySpriteRenderer.sprite = mySprites[5];
        }
        else if(angle >= 247.5 || angle < -67.5) // F
        {
            mySpriteRenderer.sprite = mySprites[6];
        }
        else // FL
        {
            mySpriteRenderer.sprite = mySprites[7];
        }
    }
}
