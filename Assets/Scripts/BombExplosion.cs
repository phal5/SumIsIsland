using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public float bombRadius = 1f;
    GameObject myIsland1;
    GameObject myIsland2;
    void Start()
    {
        myIsland1 = GameObject.Find("Island1");
        myIsland2 = GameObject.Find("Island2");
    }

    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Island2")
        {
            int n = myIsland2.transform.childCount;
            if(n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    FloatingObject cur_child = myIsland2.transform.GetChild(i).gameObject.GetComponent<FloatingObject>();
                    checkIslandFloatables(cur_child);
                }
            }
        }
    }

    private void checkIslandFloatables(FloatingObject cur_obj)
    {
        int n = cur_obj.transform.childCount;
        if (n <= 0) { return; }

        for(int i = 0; i < n; i++)
        {
            Transform cur_child = cur_obj.transform.GetChild(i).gameObject.transform;
            if (isWithinRadius(cur_child))
            {
                
            }
        }
        

    }

    private bool isWithinRadius(Transform other)
    {
        float distance = Vector3.Distance(transform.position, other.position);
        if(distance < bombRadius) // 반경 내에 있는 경우
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
