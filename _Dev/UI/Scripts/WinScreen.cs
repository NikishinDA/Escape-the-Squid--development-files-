using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Text moneyText;
    [SerializeField] private Slider weaponProgressBar;
    [SerializeField] private float progressPerLevel = 0.4f;
    private int _gunNumber;
    [SerializeField] private Image pbBackground;
    [SerializeField] private Image pbImage;
    [SerializeField] private Sprite[] gunsBackgrounds;
    [SerializeField] private Sprite[] gunsSprites;
    
    private void Awake()
    {
        nextLevelButton.onClick.AddListener(OnNextButtonClick);
        _gunNumber = PlayerPrefs.GetInt(PlayerPrefsStrings.GunNumber, 0);
        pbBackground.sprite = gunsBackgrounds[_gunNumber];
        pbImage.sprite = gunsSprites[_gunNumber];
    }

    private void Start()
    {
        moneyText.text = VarSaver.MoneyCollected + " x " + VarSaver.Multiplier + " = " + VarSaver.MoneyCollected;

        StartCoroutine(SkinProgress(1f));
        StartCoroutine(MoneyCounter());
    }

    private IEnumerator MoneyCounter()
    {
        yield return new WaitForSeconds(1.5f);
        int money = VarSaver.MoneyCollected;
        int endMoney = money * VarSaver.Multiplier;
        while (money < endMoney - 10)
        {
            money+=10;
            moneyText.text = VarSaver.MoneyCollected + " x " + VarSaver.Multiplier + " = " + money;
            yield return null;
        }
        moneyText.text = VarSaver.MoneyCollected + " x " + VarSaver.Multiplier + " = " + endMoney;
        
    }

    private  IEnumerator SkinProgress(float time)
    {
        float weaponProgress = PlayerPrefs.GetFloat(PlayerPrefsStrings.GunProgress, 0.01f);
        float endProgress = weaponProgress + progressPerLevel;
        if (endProgress >= 1)
        {
            endProgress = 1;
            PlayerPrefs.SetFloat(PlayerPrefsStrings.GunProgress, 0);
            _gunNumber = (_gunNumber + 1) % 3;
            PlayerPrefs.SetInt(PlayerPrefsStrings.GunNumber, _gunNumber);
            PlayerPrefs.SetInt(PlayerPrefsStrings.MaxAmmo, 0);
            PlayerPrefs.SetInt(PlayerPrefsStrings.UpgradeCost, 1000);
        }
        else
        {
            PlayerPrefs.SetFloat(PlayerPrefsStrings.GunProgress, endProgress);
        }
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            weaponProgressBar.value = Mathf.Lerp(weaponProgress, endProgress, t / time);
            yield return null;
        }
        
    }
    private void OnNextButtonClick()
    {
        int totalMoney = PlayerPrefs.GetInt(PlayerPrefsStrings.MoneyTotal, 0);
        totalMoney += VarSaver.MoneyCollected * VarSaver.Multiplier;
        PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal, totalMoney);
        PlayerPrefs.Save();
        SceneLoader.LoadNextLevel();
    }
}
