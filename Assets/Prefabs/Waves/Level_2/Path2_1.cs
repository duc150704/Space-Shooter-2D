using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Path2_1 : Path
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Spawn()
    {
        yield return new WaitUntil(() => wave.State == WaveSate.SPAWNINGENERMY);
        Vector3 direction = -transform.up + -transform.right;
        for (int i = 0;i < wave.TotalEnermy / 3; i++)
        {
            CreateAsteroid(speed, rotateSpeed + 30, wave.PathPoints[2].position, direction);
            yield return new WaitForSeconds(0.6f);
            CreateAsteroid(speed, rotateSpeed, wave.PathPoints[3].position, direction);
            yield return new WaitForSeconds(0.6f);
            CreateAsteroid(speed, rotateSpeed -10 , wave.PathPoints[4].position, direction);
            yield return new WaitForSeconds(0.4f);
        }
        wave.State = WaveSate.WAITING;
    }

    void CreateAsteroid(float speed, float rotateSpeed, Vector3 spawnPos, Vector3 direction)
    {
        float random = Random.Range(0.5f, 2f);
        GameObject go = Instantiate(wave.EnermyPrefs[0], spawnPos, Quaternion.identity);
        go.transform.localScale = new Vector3(random, random, random);
        if (go)
        {
            Asteroid asteroid = go.GetComponent<Asteroid>();
            asteroid.Move(speed, direction);
            asteroid.Rotate(rotateSpeed);
        }
        Destroy(go,5f);
    }
}
