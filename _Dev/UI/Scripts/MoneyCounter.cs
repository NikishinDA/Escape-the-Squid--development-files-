using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private Text counterText;
    private int moneyCollected = 0;
    private void Awake()
    {
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        moneyCollected += 100 + Random.Range(0, 100);
        VarSaver.MoneyCollected = moneyCollected;
        counterText.text = moneyCollected.ToString();
    }

    void Start()
    {
        counterText.text = "0";
    }
}
