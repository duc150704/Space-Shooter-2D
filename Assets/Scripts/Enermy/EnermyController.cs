using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public static EnermyController Instance { get; private set; }

    [SerializeField] Transform gunPos;
    [SerializeField] GameObject bullet;
    Rigidbody2D rb;

    [SerializeField] float knockBackForce;
    [SerializeField] float knockBackTime;

    bool canFire = false;
    [SerializeField]float delayShootingTime = 2f;
    float delayShootingTimeCounter = 0f;
    float freezeTime = 4f;
    float freezeTimeCounter = 0f;

    public bool CanFire
    {
        get => canFire; set => canFire = value;
    }
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(Shooting());
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        delayShootingTimeCounter += Time.deltaTime;
        if(delayShootingTimeCounter > delayShootingTime && canFire)
        {
            Shoot();
            delayShootingTimeCounter = 0f;
        }

        if (canFire) 
        {
            freezeTimeCounter += Time.deltaTime;
            if (freezeTimeCounter >= freezeTime) 
            { 
                CanFire = false;
            }
        }
    }

    private void FixedUpdate()
    {

    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(2f);

        //while (gameObject)
        //{

        //}
    }

    public void Shoot()
    {
        CreateBullet(gunPos.position);
    }

    private void CreateBullet(Vector2 pos)
    {
        GameObject go = Instantiate(bullet, pos, Quaternion.identity);
        Destroy(go, 5f);
    }


}
