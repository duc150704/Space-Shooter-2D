using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToTop : MonoBehaviour
{
    [SerializeField] float yPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    void Check()
    {
        if (gameObject.transform.position.y < yPos)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 20); 
        }
    }
}
