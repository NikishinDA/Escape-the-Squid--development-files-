using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerDetector : MonoBehaviour
{
    private List<Transform> _targets;

    [SerializeField] private AimController aimController;
    
    private void Awake()
    {
        _targets = new List<Transform>();
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
        Transform enemyTransform = other.transform;
        _targets.Add(enemyTransform);
        UpdateList();
    }

    private void OnTriggerExit(Collider other)
    {
        _targets.Remove(other.transform);
        UpdateList();
    }
    private void UpdateList()
    {
        _targets = _targets.OrderBy(target => target.position.z).ToList();
        var evt = GameEventsHandler.PlayerTargetChangeEvent;
        evt.Target = _targets.Count > 0 ? _targets[0] : null;
        EventManager.Broadcast(evt);
    }
}
