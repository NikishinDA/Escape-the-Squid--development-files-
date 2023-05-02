using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
class Level
{
    public Chunk[] chunks;
}
public class ChunkPrefabPlacer : MonoBehaviour
{
   [SerializeField] private Transform _playerTransform;
    [SerializeField] private Level[] _chunkPrefabs;
    [SerializeField] private Chunk[] _firstPrefab;
    [SerializeField] private Chunk _finalPrefab;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private int _chunkNumber;
    [SerializeField] private int _levelLength;

    private List<Chunk> _spawnedChunks;
    private bool _finishSpawned = false;
    private int _currentLength;
    private int _level = 1;
    private void Awake()
    {
        _spawnedChunks = new List<Chunk>();
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
    }
    private void Start()
    {
        _level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 1) - 1;
        if (_level > 4)
        {
            _level = Random.Range(0, 5);
        }
        _levelLength = _chunkPrefabs[_level].chunks.Length;
        VarSaver.LevelLength = _levelLength;
        foreach (Chunk ch in _firstPrefab)
        {
            _spawnedChunks.Add(ch);
        }
    }

    private void Update()
    {
        if ((!_finishSpawned) && (_playerTransform.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.position.z - _spawnDistance))
        {
            SpawnChunk();
        }

    }
    private void SpawnChunk()
    {
        Chunk newChunk;
        if (_currentLength < _levelLength)
        {
            newChunk = Instantiate(_chunkPrefabs[_level].chunks[_currentLength]);
        }
        else
        {
            newChunk = Instantiate(_finalPrefab);
            _finishSpawned = true;
        }
        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        Vector3 newChunkPos = newChunk.transform.position;
        newChunkPos.x = _playerTransform.position.x;
        newChunk.transform.position = newChunkPos;
        _spawnedChunks.Add(newChunk);
        _currentLength++;
        if (_spawnedChunks.Count > _chunkNumber)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }
    private void OnGameStart(GameStartEvent obj)
    {
        //_levelLength = obj.LevelSetLength;
    }
}
