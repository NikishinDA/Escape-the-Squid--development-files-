using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float strifeSpeed;
    [SerializeField] private float boostMultiplier;
    [SerializeField] private float boostTime = 1f;
    private CharacterController _controller;

    private float _strifeScale;
    private MeshRenderer _renderer;
    private PlayerInputManager _inputManager;

    private bool _stopped = true;
    public Transform fogObject;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputManager = GetComponent<PlayerInputManager>();
        EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);
        EventManager.AddListener<BoostPickUpEvent>(OnBoostPickUp);

    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);
        EventManager.RemoveListener<BoostPickUpEvent>(OnBoostPickUp);

    }

    private void OnBoostPickUp(BoostPickUpEvent obj)
    {
        //StopAllCoroutines();
        StartCoroutine(BoostCor(boostTime));
    }

    private IEnumerator BoostCor(float time)
    {
        forwardSpeed *= boostMultiplier;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }

        forwardSpeed /= boostMultiplier;
    }
    private void OnPlayerOnEntryPoint(PlayerOnEntryPointEvent obj)
    {
        _stopped = true;
        _controller.enabled = false;
    }

    private void OnGameOver(GameOverEvent obj)
    {
        _stopped = true;
    }

    private void OnGameStart(GameStartEvent obj)
    {
        _stopped = false;
    }
    
    private void OnPlayerDeath(PlayerDeathEvent obj)
    {
        _stopped = true;
    }
    
    private void Update()
    {
        if (!_stopped)
        {
            _strifeScale = _inputManager.MoveDelta;
            _controller.Move((Vector3.forward * forwardSpeed
                              + Vector3.right * (strifeSpeed * _strifeScale)) * Time.deltaTime);
        }
    }
}
