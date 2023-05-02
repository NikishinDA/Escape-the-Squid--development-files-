using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    private Transform _currentTarget;
    private int _layerMask;
    [SerializeField] private float rotationSpeed;
    private bool _canShoot = false;
    public Transform CurrentTarget
    {
        set => _currentTarget = value;
    }

    private void Awake()
    {
        EventManager.AddListener<PlayerTargetChangeEvent>(OnPlayerTargetChange);
        EventManager.AddListener<BulletsNumberChangeEvent>(OnBulletNumberChange);
        EventManager.AddListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerTargetChangeEvent>(OnPlayerTargetChange);
        EventManager.RemoveListener<BulletsNumberChangeEvent>(OnBulletNumberChange);
        EventManager.RemoveListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);
    }

    private void OnPlayerOnEntryPoint(PlayerOnEntryPointEvent obj)
    {
        gameObject.SetActive(false);
    }
    private void OnBulletNumberChange(BulletsNumberChangeEvent obj)
    {
        _canShoot = obj.Number > 0;
    }

    private void OnPlayerTargetChange(PlayerTargetChangeEvent obj)
    {
        _currentTarget = obj.Target;
    }

    private void Update()
    {
        if (_canShoot)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                _currentTarget
                    ? Quaternion.LookRotation(-transform.position + _currentTarget.position)
                    : Quaternion.identity, rotationSpeed * Time.deltaTime);
        }
    }
}