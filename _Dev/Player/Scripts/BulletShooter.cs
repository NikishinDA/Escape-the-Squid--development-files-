using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float maxShootAngle;
    [SerializeField] private Transform muzzleTransform;
    private Transform _currentTarget;
    [SerializeField] private int clipSize;
    private Animator _animator;
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private bool _targeted;
    private int _bulletsNumber;
    private static readonly int HasBullets = Animator.StringToHash("HasBullets");
    [SerializeField] private float boostMultiplier;
    [SerializeField] private float boostTime = 1f;
    [SerializeField] private GameObject pistolObject;
    [SerializeField] private GameObject revolverObject;
    [SerializeField] private GameObject deObject;
    private GameObject _gunObject;
    private Animator _gunAnimator;
    private void Awake()
    {
        EventManager.AddListener<PlayerTargetChangeEvent>(OnPlayerTargetChange);
        EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.AddListener<BulletsPickUpEvent>(OnBulletPickUp);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<BoostPickUpEvent>(OnBoostPickUp);
        _animator = GetComponent<Animator>();
        
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerTargetChangeEvent>(OnPlayerTargetChange);
        EventManager.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.RemoveListener<BulletsPickUpEvent>(OnBulletPickUp);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<BoostPickUpEvent>(OnBoostPickUp);

    }

    private void Start()
    {
        switch (PlayerPrefs.GetInt(PlayerPrefsStrings.GunNumber,0))
        {
            case 1:
            {
                _gunObject = revolverObject;
            }
                break;
            case 2:
            {
                _gunObject = deObject;
            }
                break;
            default:
            {
                pistolObject.SetActive(true);
            }
                break;
        }

        if (_gunObject != null)
        {
            _gunObject.SetActive(true);
            _gunAnimator = _gunObject.GetComponent<Animator>();
        }
    }

    private void OnBoostPickUp(BoostPickUpEvent obj)
    {
        
        //StopAllCoroutines();
        StartCoroutine(BoostCor(boostTime));
    }
    private IEnumerator BoostCor(float time)
    {
        _animator.speed *= boostMultiplier;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }

        _animator.speed /= boostMultiplier;
    }
    private void OnGameStart(GameStartEvent obj)
    {
        _animator.SetTrigger("Start");
        _bulletsNumber = PlayerPrefs.GetInt(PlayerPrefsStrings.MaxAmmo, 0);
        ChangeBulletsNumberEvent();
    }

    private void OnBulletPickUp(BulletsPickUpEvent obj)
    {
        _bulletsNumber += obj.Number;
        if (_bulletsNumber > clipSize)
            _bulletsNumber = clipSize;
        ChangeBulletsNumberEvent();
    }

    private void OnPlayerDeath(PlayerDeathEvent obj)
    {
        gameObject.SetActive(false);
    }

    private void OnPlayerTargetChange(PlayerTargetChangeEvent obj)
    {
        _currentTarget = obj.Target;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _bulletsNumber = clipSize;
            
            ChangeBulletsNumberEvent();
        }
        if (_currentTarget && _bulletsNumber > 0 && Vector3.Angle(transform.forward, _currentTarget.position - transform.position) <
            maxShootAngle)
        {
            _animator.SetTrigger(Shoot);
            _targeted = true;
        }

        if (_targeted && !_currentTarget)
        {
            _animator.ResetTrigger(Shoot);
            _targeted = false;
        }
    }

    private void ChangeBulletsNumberEvent()
    {
        var evt = GameEventsHandler.BulletsNumberChangeEvent;
        evt.Number = _bulletsNumber;
        evt.Max = clipSize;
        EventManager.Broadcast(evt);
        _animator.SetBool(HasBullets, _bulletsNumber > 0);
    }
    public void ShootBullet()
    {
        if (_gunAnimator)
            _gunAnimator.SetTrigger(Shoot);
        Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.LookRotation(muzzleTransform.forward));
        _bulletsNumber--;
        Taptic.Medium();
        if (_bulletsNumber <= 0)
        {
            _animator.ResetTrigger(Shoot);
            if (_gunAnimator)
                _gunAnimator.ResetTrigger(Shoot);
        }
        ChangeBulletsNumberEvent();
    }
}