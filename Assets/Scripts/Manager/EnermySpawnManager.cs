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
        WaitForSeconds w = new WaitForSeconds(0.3f);

        for (int i = rows / spaceBetweenEnermy ; i <= rows && spawnedEnermy <= wave.totalObject; i++)
        {
            for (int j = - columns / spaceBetweenEnermy; j <= columns / spaceBetweenEnermy && spawnedEnermy <= wave.totalObject; j++)
            {
                GameObject newEnermy = Instantiate(wave.enermyPrefabs, wave.spawnPos[spawnPosIndex].position, Quaternion.Euler(0, 0, 180));
                StartCoroutine(MoveEnermy(newEnermy, wave.spawnPos, wave.spawnWay, new Vector2(j * spaceBetweenEnermy, i * spaceBetweenEnermy)));
                spawnedEnermy++;
                yield return w;
            }
        }
    }

    IEnumerator MoveEnermy(GameObject newEnermy, Transform[] spawnPositions ,Transform[] targets, Vector2 finalPos)
    {
        float movingTime = 3f;
        float movingTimeCounter = movingTime;

        float yValue = newEnermy.transform.position.y;
        for (int i = 0; i < targets.Length; i++) 
        {
            movingTimeCounter = movingTime;
            while (movingTimeCounter > 0 && newEnermy !=  null)
            {
                movingTimeCounter -= Time.deltaTime;
                yValue -= Time.deltaTime * 5f;
                newEnermy.transform.position = new Vector2(Mathf.Sin(yValue),yValue);
                yield return null;
            }
            movingTimeCounter = movingTime;
            while (movingTimeCounter > 0 && newEnermy != null)
            {
                movingTimeCounter -= Time.deltaTime;
                newEnermy.transform.position = Vector2.Lerp(newEnermy.transform.position, finalPos, 0.02f);
                yield return null;
            }
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
