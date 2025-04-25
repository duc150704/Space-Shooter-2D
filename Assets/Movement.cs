using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform target;

    [SerializeField] float speed;
    float speedCounter = 0f;

    [SerializeField] Transform topVector;
    [SerializeField] Transform bottomVector;

    // Update is called once per frame
    void Update()
    {
        speedCounter += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, topVector.position) < 0.1f)
        {
            target = bottomVector;
           // speedCounter = 0f;
        } 
        if (Vector3.Distance(transform.position, bottomVector.position) < 0.1f)
        {
            target = topVector;
            //speedCounter = 0f;
        }
    }

    private void Start()
    {
        target = topVector;
        gameObject.transform.position = bottomVector.position;
    }
}
