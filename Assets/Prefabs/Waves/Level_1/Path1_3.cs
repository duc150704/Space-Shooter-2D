using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Path1_3 : Path
{
    GameObject Boss;
    [SerializeField] float bossMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Boss = Instantiate(wave.EnermyPrefs[0], wave.PathPoints[0].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss)
            Boss.gameObject.transform.position = Vector3.MoveTowards(Boss.gameObject.transform.position, wave.PathPoints[1].position, bossMoveSpeed * Time.deltaTime);
    }

}
