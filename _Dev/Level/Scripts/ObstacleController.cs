using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private bool vaultObstacle;
    [SerializeField] private int damage = 1;
    private IObstacle specialEffect;

    private void Awake()
    {
        specialEffect = transform.parent.GetComponent<IObstacle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (vaultObstacle)
        {
            EventManager.Broadcast(GameEventsHandler.PlayerVaultEvent);
        }
        else
        {
            specialEffect?.ObstacleCrash();
            var evt = GameEventsHandler.PlayerTakeDamageEvent;
            evt.Damage = damage;
            EventManager.Broadcast(evt);
        }
        gameObject.SetActive(false);
    }
}
