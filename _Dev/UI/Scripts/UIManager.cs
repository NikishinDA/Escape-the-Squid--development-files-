using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject minigameUI;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private GameObject tutorialText;
    [SerializeField] private GameObject tutorialMGText;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);

    }

    private void OnPlayerOnEntryPoint(PlayerOnEntryPointEvent obj)
    {
        overlay.SetActive(false);
        minigameUI.SetActive(true);
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 1) == 1)
        {
            StartCoroutine(FinisherTutorTimer(2f));
        }
    }

    private void OnGameOver(GameOverEvent obj)
    {        
        
        minigameUI.SetActive(false);
        if (obj.IsWin)
        {
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }
    }

    private void Start()
    {
        startScreen.SetActive(true);
    }

    private void OnGameStart(GameStartEvent obj)
    {
        startScreen.SetActive(false);
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 1) == 1)
        {
            tutorialScreen.SetActive(true);
            tutorialText.SetActive(true);
        }
        overlay.SetActive(true);
    }

    private IEnumerator FinisherTutorTimer(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        tutorialScreen.SetActive(true);
        tutorialMGText.SetActive(true);
    }
}
