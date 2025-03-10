using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    [SerializeField] AudioSource background;
    [SerializeField] AudioSource SFX;

    [SerializeField] AudioClip gunSound;

    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(Instance);
        }
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

    public void PlayGunSound()
    {
        SFX.clip = gunSound;
        SFX.PlayOneShot(gunSound);
    }
}
