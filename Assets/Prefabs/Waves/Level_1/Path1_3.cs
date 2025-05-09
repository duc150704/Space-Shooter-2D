using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Path1_3 : Path
{
    public enum BossState { moving, shooting }

    GameObject boss;
    [SerializeField] float bossMoveSpeed;

    private BossState currentState = BossState.moving;
    // Start is called before the first frame update
    void Start()
    {
        boss = Instantiate(wave.EnermyPrefs[0], wave.PathPoints[0].position, Quaternion.identity);
        StartCoroutine(IntroState());
    }

    // Update is called once per frame
    void Update()
    {
            
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
        StartCoroutine(AttackState());
    }

    IEnumerator AttackState()
    {
        yield return new WaitUntil(() => currentState == BossState.shooting);
        BossController bs = boss.GetComponent<BossController>();
        StartCoroutine(bs.Shoot1());
        yield return null;
    }

}
