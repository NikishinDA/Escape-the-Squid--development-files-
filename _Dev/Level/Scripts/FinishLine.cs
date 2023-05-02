using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private BrakeWall brokenFinish;
    [SerializeField] private GameObject quad;
    private void Start()
    {
        EventManager.Broadcast(GameEventsHandler.FinishSpawnedEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 playerPos = other.transform.position;
        var evt = GameEventsHandler.PlayerFinishPassedEvent;
        evt.PlayerPos = playerPos;
        EventManager.Broadcast(evt);
        quad.SetActive(false);
        Vector3 finishPos = brokenFinish.transform.position;
        finishPos.x = playerPos.x;
        brokenFinish.transform.position = finishPos;
        brokenFinish.ObstacleCrash();
    }
}
