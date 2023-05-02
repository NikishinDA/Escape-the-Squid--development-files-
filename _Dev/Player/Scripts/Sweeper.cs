using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{
    private void Awake()
    {
        EventManager.AddListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerOnEntryPointEvent>(OnPlayerOnEntryPoint);
    }

    private void OnPlayerOnEntryPoint(PlayerOnEntryPointEvent obj)
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.transform.parent.gameObject);
    }
}
