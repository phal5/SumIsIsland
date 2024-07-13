using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatsGenerator : MonoBehaviour
{
    public List<GameObject> FloatsObj = new List<GameObject>();
    public Transform GenPos;

    [SerializeField] float _delay = 1.75f;
    [SerializeField] float _randomness = 1.5f;
    [SerializeField] bool boost = false;
    [SerializeField] bool allowDuplicate = true;

    List<GameObject> FloatArr = new List<GameObject>();
    bool isGenerate;
    float min_delay; //최소 딜레이
    
    void Start()
    {
        isGenerate = true;
        min_delay = 0.5f;
        StartCoroutine(callGenerator());
    }

    IEnumerator callGenerator()
    {
        while (isGenerate)
        {
            if(FloatsObj.Count > 0)
            {
                generateObj();
                if (boost && _delay > min_delay)
                {
                    _delay -= Time.deltaTime * 0.01f;
                }
                float delay = _delay + Random.Range(-_randomness * 0.5f, _randomness * 0.5f);
                yield return new WaitForSeconds(delay);
            }
        }
    }

    private void generateObj()
    {
        int objIndex = Random.Range(0, FloatsObj.Count);
        GameObject newObj = GameObject.Instantiate(FloatsObj[objIndex]);
        if (!allowDuplicate) //스페셜 아이템은 중복 허용 x
        {
            FloatsObj.Remove(FloatsObj[objIndex]);
        }

        float randPos = Random.Range(-50, 50);
        newObj.transform.position = GenPos.position + new Vector3(randPos, 0, 0);

        FloatArr.Add(newObj);
    }
}
