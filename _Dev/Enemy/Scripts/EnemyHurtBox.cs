using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    private EnemyAnimationController _animationController;

    private void Awake()
    {
        _animationController = transform.parent.GetComponent<EnemyAnimationController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.PlayerTakeDamageEvent;
        evt.Damage = damage;
        EventManager.Broadcast(evt);
        Vector3 playerPos = other.transform.position;
        playerPos.x = transform.position.x;
        other.transform.position = playerPos;
        _animationController.PlayAttackAnim();
    }
}
