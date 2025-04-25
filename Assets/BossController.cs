using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField] Transform rightGun;
    [SerializeField] Transform leftGun;

    private string currentState;

    public string CurrentState
    {
        get => currentState;
        set => currentState = value;
    }


}
