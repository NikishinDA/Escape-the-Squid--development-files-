using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FinisherBulletShooter : MonoBehaviour
{
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private GameObject bulletPrefab;
    private Animator _animator;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayerInPos()
    {
        _animator.SetBool("Ready", true);
    }

    public void StopShooting()
    {
        StartCoroutine(StopDelay(2f));
    }

    private IEnumerator StopDelay(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        _animator.SetBool("Ready", false);
    }
    public void ShootBullet()
    {
        Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
        Taptic.Light();
        impulseSource.GenerateImpulse();
    }
}
