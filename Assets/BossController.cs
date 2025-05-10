using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            if (!player)
                break;
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

    public IEnumerator Shoot2()
    {
        List<GameObject> list = new List<GameObject>();
        int angle = 45;
        float speed = 5f;
        for(int i = 0; i < 4; i++)
        {
            angle -= 90;
            GameObject go = Instantiate(listProjectiles[2], middleGun.position, Quaternion.Euler(0, 0, angle));
            list.Add(go);
            yield return new WaitForSeconds(1f);
        }
        for(int i = 0; i < list.Count; i++)
        {

        }
    }

    IEnumerator MoveShoot2Rocket(GameObject go, Vector3 direction, float speed)
    {
        float time = 3f;
        float timeCounter = 0f;
        while(timeCounter <= time && go)
        {
            go.transform.position = Vector3.MoveTowards(middleGun.position, go.transform.up * 3f, speed * Time.deltaTime);
            timeCounter += Time.deltaTime;
            yield return null;
        }


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

    IEnumerator RotateGameObject(GameObject go ,float angle, float timeToRotate)
    {
        float timeToRotateCounter = 0;
        while (gameObject) {
            go.transform.rotation = Quaternion.Lerp(go.transform.rotation,Quaternion.Euler(0f, 0f, angle),timeToRotateCounter/ timeToRotate );
            timeToRotateCounter += Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator RotateGameObject(GameObject go, Vector3 startPos, Vector3 desPos, float timeToRotate)
    {
        float timeToRotateCounter = 0;

        Vector3 direction = desPos - startPos;
        float angle = Mathf.Atan2(desPos.x, direction.y);
        while (gameObject)
        {
            go.transform.rotation = Quaternion.Lerp(go.transform.rotation, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg), timeToRotateCounter / timeToRotate);
            timeToRotateCounter += Time.deltaTime;
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
