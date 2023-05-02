using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject[] bullets;
    private int lastNum = 0;
    private int currentNumber = 0;
    private int activeBullets = 0;
    private void Awake()
    {
        EventManager.AddListener<BulletsNumberChangeEvent>(OnBulletNumberChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<BulletsNumberChangeEvent>(OnBulletNumberChange);
    }

    private void OnBulletNumberChange(BulletsNumberChangeEvent obj)
    {
        text.text = obj.Number + "/" + obj.Max;
        int curNum = obj.Number;
        int delta = curNum - lastNum;
        currentNumber = Mathf.Clamp(currentNumber + delta, 0, bullets.Length);
        if (currentNumber == 0)
        {
            currentNumber = (curNum - 1) % bullets.Length + 1;
        }

        if (activeBullets < currentNumber)
        {
            for (int i = activeBullets; i < currentNumber; i++)
            {
                bullets[i].SetActive(true);
            }
        }
        else
        {
            for (int i = activeBullets - 1; i >= currentNumber; i--)
            {
                bullets[i].SetActive(false);
            }
        }

        activeBullets = currentNumber;
        lastNum = curNum;
    }
}
