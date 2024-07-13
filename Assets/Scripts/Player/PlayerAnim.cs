using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnim : MonoBehaviour
{
    public KeyCode myLeft;
    public KeyCode myRight;
    public int PlayerIndex;
    
    SpriteRenderer mySpriteRenderer;
    Transform HarpoonHead;

    public List<Sprite> mySprites = new List<Sprite>();
    float angle;
    Vector3 localScale;
    Vector3 reverseScale;

    Animator myAnimator;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        HarpoonHead = transform.parent.transform.Find("HarpoonAxis").transform;
        angle = HarpoonHead.rotation.eulerAngles.y;

        localScale = transform.localScale;
        Vector2 tmp = localScale;
        tmp.x *= -1;
        reverseScale = tmp;
    }

    void Update()
    {
        angle = HarpoonHead.rotation.eulerAngles.y;
        if (Input.GetKey(myLeft)) //���� ��ǲ�� ��
        {
            myAnimator.SetBool("isWalk", true);

            // ���� ���ʿ� ������ ���������� �ȱ�
            if (angle >= 90 && angle <= 270 && PlayerIndex == 1) //p1 ���������� ���ֱ�
            {
                Flip();
            }

            // ���� ���ʿ� ������ �������� �ȱ�
            else if ((angle > 270 || angle < 90) && PlayerIndex == 2) //p2 �������� ���ֱ�
            {
                Flip();
            }
            else { Unflip(); }
            

        }
        else if(Input.GetKey(myRight)) //������ ��ǲ�� ��
        {
            myAnimator.SetBool("isWalk", true);

            // ���� ���ʿ� ������ �������� �ȱ�
            if (angle >= 90 && angle <= 270 && PlayerIndex == 2) //p2 �������� ���ֱ�
            {
                Flip();
            }

            // ���� ���ʿ� ������ ���������� �ȱ�
            else if ((angle > 270 || angle < 90) && PlayerIndex == 1) //p1 ���������� ���ֱ�
            {
                Flip();
            }
            else { Unflip(); }

        }
        else
        {
            myAnimator.SetBool("isWalk", false);
            checkPlayerDirection();
        }
    }

    private void checkPlayerDirection()
    {
        // B, BR, R, FR, F, FL, L, BL
        int[] angles = { 0, 45, 90, 135, 180, 225, 270, 315 };

        float min = 10000;
        int min_index = 0;

        for(int i=0; i<angles.Length; i++)
        {
            float difference = Mathf.Abs(angle - angles[i]);
            if(difference < min)
            {
                min = difference;
                min_index = i;
            }
        }
        mySpriteRenderer.sprite = mySprites[min_index];
        Debug.Log(PlayerIndex +" player sprite is = "+ min_index);
    }

    void Flip()
    {
        transform.localScale = reverseScale;
    }

    void Unflip()
    {
        transform.localScale = localScale;    
    }

    
}
