using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMovements : BulletMovements
{
    Rigidbody2D rb2d;
    public Vector2 direction = Vector2.up;
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
        rb2d.velocity = direction * speed;
    }
}
