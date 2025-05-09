using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossController : MonoBehaviour
{

    [SerializeField] Transform rightGun;
    [SerializeField] Transform leftGun;
    [SerializeField] Transform middleGun;

    [SerializeField] List<GameObject> listProjectiles = new List<GameObject>();
    GameObject player;




    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerShooting>().gameObject;
        //StartCoroutine(Shoot0());
    }

    public void Shoot()
    {
        int ShootingType = Random.Range(0,2);
        switch(ShootingType)
        {
            case 0:
                
                break;
            default:
                break;
        }
    }

    public IEnumerator Shoot0()
    {
        float speed = 10f;
        for(int i = 0; i < 10; i++)
        {
            CreateProjectile(listProjectiles[0], leftGun.position, player.transform.position - leftGun.position, speed);
            CreateProjectile(listProjectiles[0], rightGun.position, player.transform.position - rightGun.position, speed);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public IEnumerator Shoot1()
    {
        float speed = 8f;
        Coroutine rotate = StartCoroutine(RotateMiddleGun());
        for(int i = 0; i < 45; i++)
        {
            CreateProjectile(listProjectiles[1], middleGun.position, middleGun.transform.up, speed);
            CreateProjectile(listProjectiles[1], middleGun.position, middleGun.transform.up + middleGun.transform.right, speed);
            CreateProjectile(listProjectiles[1], middleGun.position, -middleGun.transform.up, speed);
            CreateProjectile(listProjectiles[1], middleGun.position, middleGun.transform.up - middleGun.transform.right, speed);
            CreateProjectile(listProjectiles[1], middleGun.position, middleGun.transform.right, speed);
            CreateProjectile(listProjectiles[1], middleGun.position, -middleGun.transform.up + middleGun.transform.right, speed);
            CreateProjectile(listProjectiles[1], middleGun.position, -middleGun.transform.right, speed);
            CreateProjectile(listProjectiles[1], middleGun.position, -middleGun.transform.right - middleGun.transform.up, speed);
            yield return new WaitForSeconds(0.2f);
        }
        StopCoroutine(rotate);
        if(middleGun)
            middleGun.rotation = Quaternion.Euler(0f , 0f, 0f);
    }

    IEnumerator RotateMiddleGun()
    {
        float rotatingSpeed = 30f;
        while (gameObject)
        {
            middleGun.gameObject.transform.Rotate(0f, 0f, rotatingSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void CreateProjectile(GameObject projectile,Vector3 position ,Vector3 direction, float projectileSpeed)
    {
        if (projectile == null)
        {
            return;
        }

        float rotateAngles = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        GameObject go = Instantiate(projectile, position, Quaternion.Euler(0, 0, 180 - rotateAngles));

        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = projectileSpeed * direction.normalized;
        }
        Destroy(go, 6f);
    }
}
