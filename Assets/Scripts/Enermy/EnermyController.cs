using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    [SerializeField] Transform gunPos;
    [SerializeField] GameObject bullet;
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
