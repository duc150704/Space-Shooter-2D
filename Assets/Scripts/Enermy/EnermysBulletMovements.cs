using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermysBulletMovements : BulletMovements
{
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Fly();
    }

    protected override void Fly()
    {
        rb2d.velocity = Vector2.down * speed;
    }
}
