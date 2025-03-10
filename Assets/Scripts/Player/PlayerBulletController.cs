using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BulletController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject enm = collision.gameObject;
        if (enm.tag == "Enermy")
        {
            enm.GetComponent<EnermyHealth>().TakeDamage(damage);
        }
    }
}
