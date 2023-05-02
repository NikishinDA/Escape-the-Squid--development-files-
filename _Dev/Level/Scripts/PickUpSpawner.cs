using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private GameObject boostPrefab;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float delay;
    [SerializeField] private float spawnChance;
    [SerializeField] private float spawnOffset;
    [SerializeField] private float ammoSpawnChance;
    [SerializeField] private float moneySpawnChance;
    [SerializeField] private float boostSpawnChance;
    private float _cooldown;
    private bool _startSpawn = false ;

    private void Awake()
    {
        _cooldown = 0;
        EventManager.AddListener<FinishSpawnedEvent>(OnFinishSpawned);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinishSpawnedEvent>(OnFinishSpawned);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);

    }
    private void OnPlayerOnEntryPoint(PlayerOnEntryPointEvent obj)
    {
        gameObject.SetActive(false);
    }
    private void OnGameStart(GameStartEvent obj)
    {
        _startSpawn = true;
    }

    private void OnFinishSpawned(FinishSpawnedEvent obj)
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_startSpawn)
        {
            if (_cooldown <= 0)
            {
                if (Random.value < spawnChance)
                {
                    float rvalue = Random.value;
                    if (rvalue < ammoSpawnChance)
                    {
                        Instantiate(ammoPrefab,
                            transform.position
                            + new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, spawnDistance),
                            Quaternion.identity);
                    }
                    else if (rvalue < ammoSpawnChance + boostSpawnChance)
                    {
                        Instantiate(boostPrefab,
                            transform.position
                            + new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, spawnDistance),
                            Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(moneyPrefab,
                            transform.position
                            + new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, spawnDistance),
                            Quaternion.identity);
                    }
                }

                _cooldown = delay;
            }
            else
            {
                _cooldown -= Time.deltaTime;
            }
        }
    }
}