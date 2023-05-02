using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Text bulletCounter;
    [SerializeField] private Text moneyCounter;
    [SerializeField] private Text upgradeCostText;
    [SerializeField] private Button upgradeButton;
    private int maxAmmo = 0;
    private int moneyTotal = 0;
    private int ammoUpgradeCost = 0;
    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        maxAmmo = PlayerPrefs.GetInt(PlayerPrefsStrings.MaxAmmo, 0);
        moneyTotal = PlayerPrefs.GetInt(PlayerPrefsStrings.MoneyTotal, 0);
        ammoUpgradeCost = PlayerPrefs.GetInt(PlayerPrefsStrings.UpgradeCost, 1000);
    }

    private void OnUpgradeButtonClick()
    {
        if (moneyTotal > ammoUpgradeCost)
        {
            moneyTotal -= ammoUpgradeCost;
            PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal, moneyTotal);
            moneyCounter.text = moneyTotal.ToString();
            maxAmmo++;
            PlayerPrefs.SetInt(PlayerPrefsStrings.MaxAmmo, maxAmmo);
            bulletCounter.text = maxAmmo.ToString();
            ammoUpgradeCost = (int) (ammoUpgradeCost * 2.5f);
            PlayerPrefs.SetInt(PlayerPrefsStrings.UpgradeCost, ammoUpgradeCost);
            upgradeCostText.text = ammoUpgradeCost.ToString();
            PlayerPrefs.Save();

        }
    }

    private void Start()
    {
        bulletCounter.text = maxAmmo.ToString();
        moneyCounter.text = moneyTotal.ToString();
        upgradeCostText.text = ammoUpgradeCost.ToString();
    }

    private void OnStartButtonClick()
    {
        EventManager.Broadcast(GameEventsHandler.GameStartEvent);
    }
}
