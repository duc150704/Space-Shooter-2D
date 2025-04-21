using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;

public class Path1_2 : Path
{
    [SerializeField] Transform spawnerPos;

    List<GameObject> enermyList = new List<GameObject>();
    [SerializeField, Range(0, 1)] float fireRate;

    float timeToSwapEnermyList = 2f;
    float timeToSwapEnermyListCounter = 0f;
    private void Start()
    {
        StartCoroutine(SpawnEnermy());
    }

    private void Update()
    {
        timeToSwapEnermyListCounter += Time.deltaTime;
        if (timeToSwapEnermyListCounter > timeToSwapEnermyList)
        {
            SwapEnermyInEnermyList();
            timeToSwapEnermyListCounter = 0f;
            Fire();
        }
    }
    private void Fire()
    {
        int count = (int)Mathf.Ceil(Mathf.Lerp(0, enermyList.Count, fireRate));
        for (int i = 0; i < count; i++)
        {
            if (enermyList[i])
            {
                enermyList[i].GetComponent<EnermyController>().CanFire = true;
            }
        }
    }

    private void SwapEnermyInEnermyList()
    {
        for (int i = 0; i < enermyList.Count; i++)
        {
            int nextEnIndex = UnityEngine.Random.Range(0, enermyList.Count);
            if (enermyList[i] && enermyList[nextEnIndex])
            {
                GameObject go = enermyList[i];
                enermyList[i] = enermyList[nextEnIndex];
                enermyList[nextEnIndex] = go;
            }
        }
    }

    IEnumerator SpawnEnermy()
    {
        yield return new WaitUntil(() => wave.State == WaveSate.SPAWNINGENERMY);
        for(int i = 0; i < wave.TotalEnermy; i++)
        {
            GameObject enermy = Instantiate(wave.EnermyPrefs[0], spawnerPos.position, Quaternion.identity);
            enermyList.Add(enermy);
            StartCoroutine(MoveEnermy(enermy));
            yield return new WaitForSeconds(1 - spawnRate);
        }
        wave.State = WaveSate.WAITING;
    }

    IEnumerator MoveEnermy(GameObject enermy)
    {
        float time = 6f;
        float timeCounter = 0f;
        while (timeCounter <= time && enermy) 
        {
            enermy.transform.position = Vector3.MoveTowards(enermy.transform.position, new Vector3(15, enermy.transform.position.y, 0), 3f * Time.deltaTime);
            yield return null;
        }
        //Destroy(enermy.gameObject);
    }
}
