using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] protected Wave wave;
    
    [SerializeField, Range(0, 1)] protected float spawnRate;

}
