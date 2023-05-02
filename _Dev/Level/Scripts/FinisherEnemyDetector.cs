using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherEnemyDetector : MonoBehaviour
{
    private FinisherController _controller;
    private void Awake()
    {
        _controller = transform.parent.GetComponent<FinisherController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _controller.EnemyReachedCar();
    }
}
