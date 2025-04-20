using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float speed;
    Vector2 direction = Vector2.up;
    [SerializeField] Animator animator;

    [SerializeField] GameObject ExplEffect;

    [SerializeField] int damage;

    IEnumerator Lauch()
    {
        rb.AddForce(- direction * speed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        animator.enabled = true;
        rb.velocity = direction * speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Lauch());
        Invoke("Destroy", 1.4f);
        Destroy(gameObject, 1.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Destroy()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enermy");
        foreach (GameObject item in list)
        {
            item.GetComponent<EnermyHealth>().TakeDamage(damage);
        }
        Instantiate(ExplEffect, gameObject.transform.position, Quaternion.identity);
    }
}
