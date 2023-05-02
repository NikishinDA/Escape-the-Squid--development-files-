using System;
using System.Collections;
using System.Collections.Generic;
using AtmosphericHeightFog;
using com.adjust.sdk;
using GameAnalyticsSDK;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject finisherPrefab;
    [SerializeField] private float finisherSpawnDistance;
    [SerializeField] private HeightFogGlobal fog;
    private float _playTimer;
    private void Awake()
    {
        EventManager.AddListener<PlayerFinishPassedEvent>(OnFinishPassed);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        GameAnalytics.Initialize();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerFinishPassedEvent>(OnFinishPassed);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
    }
    private void OnGameStart(GameStartEvent obj)
    {
        int level = PlayerPrefs.GetInt("Level", 1);
        GameAnalytics.NewProgressionEvent (
            GAProgressionStatus.Start,
            "Level_" + level);
        StartCoroutine(Timer());
    }
    private void OnGameOver(GameOverEvent obj)
    {
        if (!obj.IsWin)
        {
            int level = PlayerPrefs.GetInt("Level", 1);
            var status = GAProgressionStatus.Fail;
            GameAnalytics.NewProgressionEvent(
                status,
                "Level_" + level,
                "PlayTime_" + Mathf.RoundToInt(_playTimer));
        }
    }
    private void OnFinishPassed(PlayerFinishPassedEvent obj)
    {
        Instantiate(finisherPrefab, new Vector3(obj.PlayerPos.x , -1.2f, obj.PlayerPos.z + finisherSpawnDistance),
            Quaternion.identity);
        StartCoroutine(DecreaseFog(3f));
        int level = PlayerPrefs.GetInt("Level", 1);
        var status = GAProgressionStatus.Complete;
        GameAnalytics.NewProgressionEvent(
            status,
            "Level_" + level,
            "PlayTime_" + Mathf.RoundToInt(_playTimer));

    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            SceneLoader.ReloadLevel();
        }
    }
#endif
    private IEnumerator DecreaseFog(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            fog.fogIntensity = Mathf.Lerp(1, 0, t / time);
            yield return null;
        }
        fog.fogIntensity = 0;
    } 
    private IEnumerator Timer()
    {
        for (;;)
        {
            _playTimer += Time.deltaTime;
            yield return null;
        }
    }
}