using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyHealth : Health
{
    [SerializeField] GameObject ExplEffect;
    // Start is called before the first frame update

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {

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
            GameObject go =  Instantiate(ExplEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(go,0.5f);
        }
    }
}
