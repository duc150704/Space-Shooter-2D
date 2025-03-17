using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyHealth : Health
{


    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;

     [SerializeField] Collider2D col;

    [SerializeField] GameObject ExplEffect;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            AudioManager.Instance.PlayEnermyExplSound();
            Instantiate(ExplEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
