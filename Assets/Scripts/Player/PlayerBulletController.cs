using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BulletController
{
     Animator animator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enermy")
        {
            Transform en = collision.gameObject.transform;

            collision.gameObject.GetComponent<EnermyHealth>().TakeDamage(damage);
            animator.SetBool("Boom", true);

            gameObject.GetComponent<PlayerBulletMovements>().direction = Vector2.zero;

            Destroy(gameObject, 0.2f);
        }
    }
}
