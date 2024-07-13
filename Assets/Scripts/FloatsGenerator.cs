using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatsGenerator : MonoBehaviour
{
    public List<GameObject> FloatsObj = new List<GameObject>();
    public Transform GenPos;

    [SerializeField] float _delay = 1.75f;
    [SerializeField] float _randomness = 1.5f;

    List<GameObject> FloatArr = new List<GameObject>();
    bool isGenerate;
    
    void Start()
    {
        isGenerate = true;
        StartCoroutine(callGenerator());
    }

    IEnumerator callGenerator()
    {
        while (isGenerate)
        {
            generateObj();
            float delay = _delay + Random.Range(-_randomness * 0.5f, _randomness * 0.5f);
            yield return new WaitForSeconds(delay);
        }
    }

    private void generateObj()
    {
        int objIndex = Random.Range(0, FloatsObj.Count);
        GameObject newObj = GameObject.Instantiate(FloatsObj[objIndex]);

        float randPos = Random.Range(-50, 50);
        newObj.transform.position = GenPos.position + new Vector3(randPos, 0, 0);

        FloatArr.Add(newObj);
    }
}
