using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireFenceFallDown : MonoBehaviour,IObstacle
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ObstacleCrash()
    {
        _animator.SetTrigger("Fall");
    }
}
