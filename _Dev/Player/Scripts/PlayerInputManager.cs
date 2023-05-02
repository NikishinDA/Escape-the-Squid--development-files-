using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private float touchDeltaScale;

    private float _downPosX;
    private float _moveDelta = 0;

    private bool _active = true;
    public float MoveDelta
    {
        get => _moveDelta;
    }

    private void Awake()
    {
        EventManager.AddListener<PlayerFinishPassedEvent>(OnPlayerPassFinish);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerFinishPassedEvent>(OnPlayerPassFinish);
    }

    private void OnPlayerPassFinish(PlayerFinishPassedEvent obj)
    {
        _active = false;
        _moveDelta = 0;
    }

    void Update()
    {
        if (_active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _downPosX = Input.mousePosition.x / Screen.width;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _moveDelta = 0;
            }
            else if (Input.GetMouseButton(0))
            {
                _moveDelta = (Input.mousePosition.x / Screen.width - _downPosX);
                _moveDelta *= touchDeltaScale;
                if (_moveDelta > 1)
                {
                    _moveDelta = 1;
                }
                else if (_moveDelta < -1)
                {
                    _moveDelta = -1;
                }

            }
        }
    }
}
