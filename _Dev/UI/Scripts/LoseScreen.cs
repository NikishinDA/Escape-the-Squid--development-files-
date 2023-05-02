using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Text moneyText;
    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void Start()
    {
        moneyText.text = VarSaver.MoneyCollected.ToString();
    }
    private void OnRestartButtonClick()
    {
        int totalMoney = PlayerPrefs.GetInt(PlayerPrefsStrings.MoneyTotal, 0);
        totalMoney += VarSaver.MoneyCollected;
        PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal, totalMoney);
        PlayerPrefs.Save();
        SceneLoader.ReloadLevel();
    }
}
