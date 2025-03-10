using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform centerGun;
    [SerializeField] Transform leftGun;
    [SerializeField] Transform rightGun;

    [SerializeField] int gunPower;

    [SerializeField] float knockBackForce;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.IsShootButtonPressed())
        {
            Shoot();
            AudioManager.Instance.PlayGunSound();
            StartCoroutine(KnockBack());
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
