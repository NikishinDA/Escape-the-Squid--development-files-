using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private int killsForFullBar;
    [SerializeField] private Animator[] pbEffects;
    private float _progress;
    private static readonly int Activate = Animator.StringToHash("Activate");
    [SerializeField] private GameObject mpEffect;
    [SerializeField] private Sprite[] mpSprites;
    private Animator _mpAnimator;
    [SerializeField] private Image mpImage;
    private static readonly int Show = Animator.StringToHash("Show");
    private int stage = 0;

    private void Awake()
    {
        _mpAnimator = mpEffect.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.AddListener<FinisherEnemyKilledEvent>(OnEnemyKilled);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherEnemyKilledEvent>(OnEnemyKilled);
    }

    private void OnEnemyKilled(FinisherEnemyKilledEvent obj)
    {
        _progress += 1f / killsForFullBar;
        if (stage == 2 && _progress >= 1f)
        {
            stage++;
            VarSaver.Multiplier = 4;
            pbEffects[2].SetTrigger(Activate);
            mpImage.sprite = mpSprites[1];  
            mpImage.SetNativeSize();

            _mpAnimator.SetTrigger(Show);
        }
        else if (stage == 1 &&_progress >= 2f / 3)
        {
            stage++;
            VarSaver.Multiplier = 3;
            pbEffects[1].SetTrigger(Activate);
            mpImage.sprite = mpSprites[0];
            mpImage.SetNativeSize();
            _mpAnimator.SetTrigger(Show);
        }
        else if (stage == 0 && _progress >= 1f / 3)
        {
            stage++;
            VarSaver.Multiplier = 2;
            pbEffects[0].SetTrigger(Activate);
            _mpAnimator.SetTrigger(Show);
        }
        
    }

    private void Update()
    {
        progressBar.value = Mathf.Lerp(progressBar.value, _progress, Time.deltaTime);
    }
}
