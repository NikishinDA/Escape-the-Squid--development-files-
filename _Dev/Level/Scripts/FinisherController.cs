using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour
{
    [SerializeField] private GameObject minigameCamera;
    [SerializeField] private FinisherBulletShooter bulletShooter;
    private FinisherEnemySpawner _enemySpawner;
    private Animator _animator;
    [SerializeField] private float gameTime;
    [SerializeField] private Transform carTransform;
    [SerializeField] private GameObject target;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemySpawner = GetComponent<FinisherEnemySpawner>();
    }

    public void SetCameraInPos(Transform player)
    {
        minigameCamera.SetActive(true);
        _animator.SetTrigger("SetInPos");
        player.SetParent(carTransform);
    }

    public void PlayerInPos()
    {
        StartCoroutine(WaitTimer(1.5f));
        StartCoroutine(MinigameTimer());
    }

    public void EnemyReachedCar()
    {
        StopGame();
    }

    private IEnumerator WaitTimer(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        target.SetActive(true);
        _enemySpawner.StartSpawn();
        bulletShooter.PlayerInPos();
        
    }
    private IEnumerator MinigameTimer()
    {
        for (float t = 0; t < gameTime; t += Time.deltaTime)
        {
            yield return null;
        }
        StopGame();
    }

    private void StopGame()
    {
        _animator.SetTrigger("End");
        bulletShooter.StopShooting();
        _enemySpawner.StopSpawn();
        StartCoroutine(PopupTimer());
    }

    private IEnumerator PopupTimer()
    {
        yield return new WaitForSeconds(3f);
        var evt = GameEventsHandler.GameOverEvent;
        evt.IsWin = true;
        EventManager.Broadcast(evt);
    }
}
