using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Path1_1 : Path
{
    List<GameObject> enermyList = new List<GameObject>();
    int spaceBetweenEnermy = 2;
    [SerializeField, Range(0, 1)] float fireRate;

    float timeToSwapEnermyList = 2f;
    float timeToSwapEnermyListCounter = 0f;
    [SerializeField] TextMeshProUGUI wavesNameTextMPGUI;


    private void Start()
    {
        StartCoroutine(Excute());
    }

    private void Update()
    {
        timeToSwapEnermyListCounter += Time.deltaTime;
        if(timeToSwapEnermyListCounter > timeToSwapEnermyList)
        {
            SwapEnermyInEnermyList();
            timeToSwapEnermyListCounter = 0f;
            Fire();
        }
    }
    private void Fire()
    {
        int count = (int)Mathf.Ceil(Mathf.Lerp(0, enermyList.Count, fireRate));
        for(int i = 0; i < count; i++)
        {
            if (enermyList[i])
            {
                enermyList[i].GetComponent<EnermyController>().CanFire = true;
            }
        }
    }

    private void SwapEnermyInEnermyList()
    {
        for(int i = 0; i < enermyList.Count; i++)
        {
            int nextEnIndex = Random.Range(0, enermyList.Count);
            if (enermyList[i] && enermyList[nextEnIndex])
            {
                GameObject go = enermyList[i];
                enermyList[i] = enermyList[nextEnIndex];
                enermyList[nextEnIndex] = go;
            }

        }
    }
    IEnumerator Excute()
    {
        yield return new WaitUntil(() => wave.State == WaveSate.SPAWNINGENERMY);

        StartCoroutine(SpawnRightToLeft());
        StartCoroutine(SpawnLeftToRight());
    }

    IEnumerator SpawnRightToLeft()
    {
        int total = wave.TotalEnermy / 2;
        for(int i = 7; i > -7 && total > 0; i -= spaceBetweenEnermy)
        {
            for (int j = -1; j > -12 && total > 0; j -= spaceBetweenEnermy) { 
                GameObject enermy = Instantiate(wave.EnermyPrefs[0], wave.PathPoints[1].position, Quaternion.Euler(0, 0, 180));
                enermyList.Add(enermy);
                total--;
                StartCoroutine(MoveEnermy(enermy, wave.PathPoints[1].position, wave.PathPoints[3].position, new Vector3(j , i ), 1f));
                yield return new WaitForSeconds(1 - spawnRate);
            }
        }
    }

    IEnumerator SpawnLeftToRight()
    {
        int total = wave.TotalEnermy /2;
        for (int i = 7; i > -7 && total > 0; i -= spaceBetweenEnermy)
        {
            for (int j = 1; j <= 12 && total > 0; j += spaceBetweenEnermy)
            {
                GameObject enermy = Instantiate(wave.EnermyPrefs[0], wave.PathPoints[0].position, Quaternion.Euler(0, 0, 180));
                enermyList.Add(enermy);
                total--;
                StartCoroutine(MoveEnermy(enermy, wave.PathPoints[0].position, wave.PathPoints[2].position, new Vector3(j, i), 0));
                yield return new WaitForSeconds(1 - spawnRate);
            }
        }

        wave.State = WaveSate.WAITING;
    }

    IEnumerator MoveEnermy(GameObject enermy, Vector3 startPos, Vector3 finalPos, Vector3 desPos, float phi)
    {
        float y = startPos.y;

        while (y >= finalPos.y && enermy) 
        {
            enermy.transform.position = new Vector3((1.5f* Mathf.Sin(y + Mathf.PI * phi)) + startPos.x, y, enermy.transform.position.z);
            y -= Time.deltaTime * 7;
            yield return null;
        }

        if (enermy)
        {
            StartCoroutine(Move(enermy, enermy.transform.position, desPos));
        }
    }

    IEnumerator Move(GameObject enermy, Vector3 start, Vector3 des)
    {
        float time = 0.5f;
        float timeCounter = 0;

        while (timeCounter <= time && enermy) 
        { 
            enermy.transform.position = Vector3.Lerp(start , des , timeCounter / time);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }
}
