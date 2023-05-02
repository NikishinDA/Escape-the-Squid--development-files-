using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.Broadcast(GameEventsHandler.MoneyCollectEvent);
        Taptic.Success();
        transform.parent.gameObject.SetActive(false);
    }
}
