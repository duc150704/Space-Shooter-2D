using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Path1_3 : Path
{
    public enum BossState { moving, shooting }

    GameObject boss;
    [SerializeField] float bossMoveSpeed;


    int randomNum = 1;
    private BossState currentState = BossState.moving;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    IEnumerator Move()
    {
        yield return new WaitUntil(() => wave.State == WaveSate.SPAWNINGENERMY);
        boss = Instantiate(wave.EnermyPrefs[0], wave.PathPoints[0].position, Quaternion.identity);
        StartCoroutine(IntroState());
        while (boss)
        {
            yield return new WaitUntil(() => currentState == BossState.shooting);
            StartCoroutine(AttackState());
            yield return new WaitUntil(() => currentState == BossState.moving);
            StartCoroutine(MoveState());
        }
    }

    IEnumerator IntroState()
    {
        float time = 3f;
        float timeCounter = 0f;
        Rigidbody2D rigidbody2D = boss.GetComponent<Rigidbody2D>();

        while (boss && timeCounter <= time)
        {
            boss.transform.position = Vector3.Lerp(wave.PathPoints[0].position, wave.PathPoints[1].position, timeCounter / time);
            timeCounter += Time.deltaTime;
            yield return null;
        }
        currentState = BossState.shooting;
    }

    IEnumerator MoveState()
    {
        float speed = 3f;
        int random = Random.Range(1, wave.PathPoints.Length);

        int loop = 0;

        while(random == randomNum && loop <= 10)
        {
            random = Random.Range(1, wave.PathPoints.Length);
            loop++;
        }
        while (boss && Vector3.Distance(boss.gameObject.transform.position, wave.PathPoints[random].position) > 0.1f)
        {
            boss.gameObject.transform.position = Vector3.MoveTowards(boss.gameObject.transform.position, wave.PathPoints[random].position, speed * Time.deltaTime);
            yield return null;
        }
        randomNum = random;
        currentState = BossState.shooting;
    }
    IEnumerator AttackState()
    {
        yield return new WaitUntil(() => currentState == BossState.shooting);
        BossController bs = boss.GetComponent<BossController>();
        if (randomNum == 1)
            yield return StartCoroutine(bs.Shoot1());
        else
            yield return StartCoroutine(bs.Shoot0());
        currentState = BossState.moving;
    }

}
