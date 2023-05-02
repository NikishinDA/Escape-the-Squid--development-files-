using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InChunkSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnRect;

    [SerializeField] private int numOfSpawns;

    //[SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject ammoPickUpPrefab;
    private float _cooldown;
    [SerializeField] private int cols;
    [SerializeField] private int rows;

    private void Start()
    {
        for (int i = 0; i < numOfSpawns; i++)
        {
            Transform transformGO = Instantiate(obstaclePrefab, transform).transform;
            transformGO.localPosition = new Vector3(Random.Range(-spawnRect.x, spawnRect.x), 0,
                Random.Range(-spawnRect.y, spawnRect.y));
            transformGO.localRotation = Quaternion.LookRotation(Vector3.back);
        }

        if (Random.value < 0.3f)
        {
            Transform transformGO = Instantiate(ammoPickUpPrefab, transform).transform;
            transformGO.localPosition = new Vector3(Random.Range(-spawnRect.x, spawnRect.x), transformGO.localPosition.y,
                Random.Range(-spawnRect.y, spawnRect.y));
        }
    }
}