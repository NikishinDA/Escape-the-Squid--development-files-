using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherGunController : MonoBehaviour
{
    [SerializeField] private Transform currentTarget;
    
    private void Update()
    {
        transform.LookAt(currentTarget);
    }

    
}
