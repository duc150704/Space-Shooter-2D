using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excute : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
