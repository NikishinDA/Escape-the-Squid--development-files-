using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody _rb;
    private EnemyAnimationController _animationController;

    [SerializeField]
    private float bodyTime = 15f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animationController = GetComponent<EnemyAnimationController>();
        EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnGameOver(GameOverEvent obj)
    {
        speed = 0;
    }

    private void OnPlayerDeath(PlayerDeathEvent obj)
    {
        speed = 0;
    }

    private void Update()
    {
        _rb.MovePosition(_rb.position + transform.forward * (speed * Time.deltaTime));
    }

    public void EnemyDeath()
    {
        speed = 0;
        _animationController.PlayDeathAnim();
        EventManager.Broadcast(GameEventsHandler.FinisherEnemyKilledEvent);
        StartCoroutine(BodyDeletionTimer(bodyTime));
    }

    private IEnumerator BodyDeletionTimer(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
