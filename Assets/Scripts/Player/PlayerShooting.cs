using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform centerGun;
    [SerializeField] Transform leftGun;
    [SerializeField] Transform rightGun;
    [SerializeField] Transform rocketPos;
    [SerializeField] GameObject rocketPrefab;

    [SerializeField] int gunPower;

    [SerializeField] float knockBackForce;

    Rigidbody2D rb2d;

    [SerializeField] float freezeTime = 0.2f;
    float freezeTimeCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        freezeTimeCounter += Time.deltaTime;
        if (InputManager.Instance.IsShootButtonPressed() && freezeTimeCounter >= freezeTime)
        {
            Shoot();
            freezeTimeCounter = 0f;
            AudioManager.Instance.PlayGunSound();
            StartCoroutine(KnockBack());
        }
        if (InputManager.Instance.IsLaunchingRocket())
        {
            GameObject go = Instantiate(rocketPrefab, rocketPos.position, Quaternion.identity);
            Destroy(go, 3f);
        }
    }

    void Shoot()
    {
        switch (gunPower) 
        {
            case 1:
                CreateBullet(centerGun);
                break;
            case 2:
                CreateBullet(leftGun);
                CreateBullet(rightGun);
                break;
            case 3:
                CreateBullet(centerGun);
                CreateBullet(leftGun);
                CreateBullet(rightGun);
                break;
            default:
                CreateBullet(centerGun);
                break;
        }
    }

    void CreateBullet(Transform pos)
    {
        GameObject newObj = Instantiate(Bullet, pos.position, Quaternion.identity);
        Destroy(newObj, 3);
    }

    IEnumerator KnockBack()
    {
        rb2d.AddForce(Vector2.down * knockBackForce * 2f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rb2d.velocity = Vector2.zero;
    }
}
