using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinisherEnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float spawnOffset;
    [SerializeField] private int rows;
    [SerializeField] private float timeInterval;
    [SerializeField] private GameObject[] enemyPrefabs;
    private List<Vector3> _spawnPoints;
    private int _lastPoint = -1;
    private void Start()
    {
        _spawnPoints = new List<Vector3>();
        for (int i = 0; i < rows; i++)
        {
            Vector3 spawnPoint = spawnTransform.position;
            spawnPoint.x += 2 * spawnOffset / (rows-1) * i - spawnOffset;
            _spawnPoints.Add(spawnPoint);
        }
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnCor());
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }
    private IEnumerator SpawnCor()
    {
        while (true)
        {
            int point = _lastPoint;
            while (point == _lastPoint)
            {
                point = Random.Range(0, rows);
            }

            _lastPoint = point;

            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], _spawnPoints[point],
                Quaternion.LookRotation(Vector3.back));
            for (float t = 0; t < timeInterval; t += Time.deltaTime)
            {
                yield return null;
            }

            yield return null;
        }
    }
}