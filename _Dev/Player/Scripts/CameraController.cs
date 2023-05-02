using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineVirtualCamera actionCamera;
    [SerializeField] private float maxAngle;
    private float _rotationScale = 0;
    private PlayerInputManager _inputManager;
    private bool _moving = false;
    private Animator _cameraAnimator;
    [SerializeField] private NoiseSettings noiseLimp;
    private NoiseSettings standartNoise;
    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        _cameraAnimator = actionCamera.GetComponent<Animator>();
        standartNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = null;
        EventManager.AddListener<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
        EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.AddListener<PlayerVaultEvent>(OnPlayerVault);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
        EventManager.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.RemoveListener<PlayerVaultEvent>(OnPlayerVault);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
    }

    private void OnGameStart(GameStartEvent obj)
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = standartNoise;

    }

    private void OnPlayerVault(PlayerVaultEvent obj)
    {
        actionCamera.gameObject.SetActive(true);
    }

    private void OnPlayerDeath(PlayerDeathEvent obj)
    {
        _moving = false;
        actionCamera.gameObject.SetActive(true);
        //virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().enabled = false;
    }

    private void OnPlayerTakeDamage(PlayerTakeDamageEvent obj)
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = noiseLimp;
    }

    public void ResetNoise()
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = standartNoise;
    }
    private void Start()
    {
        _moving = true;
    }

    private void Update()
    {
        if (_moving)
        {
            _rotationScale = -_inputManager.MoveDelta;
            Vector3 rot = virtualCamera.transform.rotation.eulerAngles;
            rot.z = maxAngle * _rotationScale;
            virtualCamera.transform.rotation = Quaternion.Euler(rot);
        }
    }
}