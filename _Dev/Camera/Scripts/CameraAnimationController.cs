using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraAnimationController : MonoBehaviour
{
    private Animator _cameraAnimator;
    [SerializeField] private CinemachineVirtualCamera actionCamera;
    private static readonly int Vault = Animator.StringToHash("Vault");
    private static readonly int Fall = Animator.StringToHash("Fall");
    [SerializeField] private ParticleSystem moneyEffect;
    [SerializeField] private ParticleSystem ammoEffect;
    [SerializeField] private ParticleSystem boostEffect;

    private void Awake()
    {
        _cameraAnimator = GetComponent<Animator>();
        EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.AddListener<PlayerVaultEvent>(OnPlayerVault);
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.AddListener<BulletsPickUpEvent>(OnAmmoPickUp);
        EventManager.AddListener<BoostPickUpEvent>(OnBoostPickUp);
    }


    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.RemoveListener<PlayerVaultEvent>(OnPlayerVault);
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.RemoveListener<BulletsPickUpEvent>(OnAmmoPickUp);
        EventManager.RemoveListener<BoostPickUpEvent>(OnBoostPickUp);
    }

    private void OnBoostPickUp(BoostPickUpEvent obj)
    {
        boostEffect.Play();
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        moneyEffect.Play();
    }

    private void OnAmmoPickUp(BulletsPickUpEvent obj)
    {
        ammoEffect.Play();
    }

    private void OnPlayerVault(PlayerVaultEvent obj)
    {
        _cameraAnimator.SetTrigger(Vault);
    }

    private void OnPlayerDeath(PlayerDeathEvent obj)
    {
        _cameraAnimator.SetTrigger(Fall);
    }
    public void OnDeathAnimEnd()
    {
        var evt = GameEventsHandler.GameOverEvent;
        evt.IsWin = false;
        EventManager.Broadcast(evt);
    }

    public void OnVaultAnimEnd()
    {
        actionCamera.gameObject.SetActive(false);
        _cameraAnimator.ResetTrigger(Vault);
    }
}