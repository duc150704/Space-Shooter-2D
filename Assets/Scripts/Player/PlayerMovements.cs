using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
//    Rigidbody2D rb2d;
    [SerializeField] float speed;
    [SerializeField] Animator EngineAnim;

    [SerializeField] GameObject PlayerExpl;

    // Start is called before the first frame update
    void Start()
    {
 //       rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        if(Vector2.Distance(gameObject.transform.position, worldPosition) > 0.1f)
        {
            EngineAnim.SetBool("IsPowering", true);
        } else
        {
            EngineAnim.SetBool("IsPowering", false);
        }
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, worldPosition, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enermy")
        {
            Destroy(collision.gameObject);
            GameObject go = Instantiate(PlayerExpl, transform.position, Quaternion.identity);
            AudioManager.Instance.PlayPlayerExplSounds();
            Destroy(go, 1f);
            Destroy(gameObject);
        }
    }
}
