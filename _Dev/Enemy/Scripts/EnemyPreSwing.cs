using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreSwing : MonoBehaviour
{
    private EnemyAnimationController _animationController;

    private void Awake()
    {
        _animationController = transform.parent.GetComponent<EnemyAnimationController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _animationController.PreSwingWeapon();
    }
}
