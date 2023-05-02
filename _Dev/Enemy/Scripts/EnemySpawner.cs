using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnRect;
    [SerializeField] private int numOfSpawns;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform spawnPoint;
    private float _cooldown;
    [SerializeField] private int cols;
    [SerializeField] private int rows;

    private void Start()
    {
        for (int i = 0; i < numOfSpawns; i++)
        {
            Transform transformGO =
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform).transform;
            transformGO.localPosition = new Vector3(Random.Range(-spawnRect.x, spawnRect.x), 0,
                Random.Range(-spawnRect.y, spawnRect.y)); //, Quaternion.LookRotation(Vector3.back));
        }
    }

    private void Update()
    {
        /*if (_cooldown <= 0)
        {
            for (int i = 0; i < numOfSpawns; i++)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                    spawnPoint.position + new Vector3(Random.Range(-spawnRect.x, spawnRect.x), 0,
                        Random.Range(-spawnRect.y, spawnRect.y)), Quaternion.LookRotation(Vector3.back));
            }

            _cooldown = timeBetweenSpawns;
        }
        else
        {
            _cooldown -= Time.deltaTime;
        }*/
    }
}