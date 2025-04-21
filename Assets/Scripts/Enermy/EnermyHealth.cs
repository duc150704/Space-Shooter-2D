using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyHealth : Health
{
    Rigidbody2D rb;

    [SerializeField] GameObject ExplEffect;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
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
