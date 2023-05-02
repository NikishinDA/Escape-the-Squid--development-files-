using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Slider progressBar;
    private float _progress = 0;
    private int _levelLength = 1;
    private void Awake()
    {
        EventManager.AddListener<PlayerProgressEvent>(OnPlayerProgress);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerProgressEvent>(OnPlayerProgress);
    }
    
    private void Start()
    {
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 1);
        levelText.text = "LEVEL " + level;
        _levelLength = VarSaver.LevelLength + 1;
    }
    
    private void OnPlayerProgress(PlayerProgressEvent obj)
    {
        _progress += 1f / _levelLength;
    }

    private void Update()
    {
        progressBar.value = Mathf.Lerp(progressBar.value, _progress, Time.deltaTime / 2);
    }
}
