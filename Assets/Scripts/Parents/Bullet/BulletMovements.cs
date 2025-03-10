using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletMovements : MonoBehaviour
{
    [SerializeField] protected float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    protected abstract void Fly();
}
