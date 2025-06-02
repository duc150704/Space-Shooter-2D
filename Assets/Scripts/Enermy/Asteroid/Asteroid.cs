using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    Rigidbody2D rb2d;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float speed, Vector3 diretion)
    {
        rb2d.velocity = speed * diretion;
    }

    public void Rotate(float rotateSpeed)
    {
        StartCoroutine(RotateObj(rotateSpeed));
    }

    IEnumerator RotateObj(float speed)
    {
        while (gameObject)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
            yield return null;
        }
    }
}
