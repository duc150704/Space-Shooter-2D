using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnermySpawnManager : MonoBehaviour
{
    public enum SpawnState { SHOWINGNAME, SPAWNING }
    [SerializeField] TextMeshProUGUI waveName;
    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPos;
    float timeBetweenWaves = 3f;

    private SpawnState state = SpawnState.SHOWINGNAME;

    // Start is called before the first frame update
    void Start()
    {
        if (waves.Length == 0)
        {
            Debug.LogError("There is no wave");
        } else
        {
            StartCoroutine(SpawnEnermy());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnermy()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < waves.Length; i++)
        {
            yield return new WaitUntil(() => state == SpawnState.SHOWINGNAME);
            StartCoroutine(ShowingWaveName(waves[i]));
            yield return new WaitUntil(() => state == SpawnState.SPAWNING);
            StartCoroutine(SpawningEnermy(waves[i]));
            yield return new WaitUntil(() => !AnyEnermyIsAlive());
            WaveCompleted();
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
    IEnumerator ShowingWaveName(Wave wave)
    {
        waveName.text = wave.name;
        waveName.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        waveName.gameObject.SetActive(false);
        state = SpawnState.SPAWNING;
    }

    IEnumerator SpawningEnermy(Wave wave)
    {
        int spawnedEnermy = 0;
        int spawnPosIndex = 0;
        int rows = 2;
        int columns = 9;
        int spaceBetweenEnermy = 2;
        WaitForSeconds w = new WaitForSeconds(0.5f);

        for (int i = rows / spaceBetweenEnermy ; i <= rows && spawnedEnermy <= wave.totalObject; i++)
        {
            for (int j = - columns / spaceBetweenEnermy; j <= columns / spaceBetweenEnermy && spawnedEnermy <= wave.totalObject; j++)
            {
                GameObject newEnermy = Instantiate(wave.enermyPrefabs, spawnPos[spawnPosIndex].position, Quaternion.identity);
                StartCoroutine(MoveEnermy(newEnermy, new Vector2(j * spaceBetweenEnermy, i * spaceBetweenEnermy)));
                spawnedEnermy++;
                yield return null;
            }
        }
    }

    IEnumerator MoveEnermy(GameObject newEnermy,Vector2 target)
    {
        float MovingTime = 1f;
        float MovingTimeCount = 0f;
        //float speed = 0.5f;

        while (MovingTimeCount < MovingTime) 
        {
            if(newEnermy == null) yield break;
            newEnermy.transform.position = Vector2.Lerp(newEnermy.transform.position, target, MovingTimeCount/MovingTime);
            MovingTimeCount += Time.deltaTime;
            yield return null;
        }
    }

    bool AnyEnermyIsAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enermy"))
        {
            return true;
        }
        return false;
    }

    void WaveCompleted()
    {
        state = SpawnState.SHOWINGNAME;
        Debug.Log("WaveComplecated");
    }
}
