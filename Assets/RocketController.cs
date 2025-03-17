using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float speed;
    Vector2 direction = Vector2.up;
    [SerializeField] Animator animator;
    public float max;

    IEnumerator Lauch()
    {
        rb.AddForce((- direction + new Vector2(Random.Range(-0.5f, 0.1f), 0)).normalized * speed * 100, ForceMode2D.Force);
        yield return new WaitForSeconds(0.3f);
        animator.enabled = true;
        rb.velocity = Vector2.zero;
        //yield return new WaitForSeconds(0.1f);
        rb.AddForce(direction * speed, ForceMode2D.Impulse);

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Lauch());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
