using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public static EnermyController Instance { get; private set; }

    [SerializeField] Transform gunPos;
    [SerializeField] GameObject bullet;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float knockBackForce;
    [SerializeField] float knockBackTime;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        CreateBullet(gunPos.position);
    }

    private void CreateBullet(Vector2 pos)
    {
        GameObject go = Instantiate(bullet, pos, Quaternion.identity);
        Destroy(go, 2f);
    }


}
