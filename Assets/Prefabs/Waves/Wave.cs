using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] string waveName;
    [SerializeField] int totalEnermy;
    [SerializeField] GameObject[] enermyPrefs;
    [SerializeField] Transform[] pathPoints;

    WaveSate state;

    public string WaveName 
    { 
        get => waveName;
        set { waveName = value; } 
    }
    public int TotalEnermy
    {
        get => totalEnermy;
        set { totalEnermy = value; }
    }
    public GameObject[] EnermyPrefs
    {
        get => enermyPrefs;
        set { enermyPrefs = value; }
    }
    public Transform[] PathPoints
    {
        get => pathPoints;
        set { pathPoints = value; }
    }
    public WaveSate State
    {
        get => state;
        set { state = value; }
    }
}
