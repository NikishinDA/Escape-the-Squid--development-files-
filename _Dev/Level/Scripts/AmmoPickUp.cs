using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] private int bulletNumber = 3;
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.BulletsPickUpEvent;
        evt.Number = bulletNumber;
        EventManager.Broadcast(evt);
        Taptic.Success();
        transform.parent.gameObject.SetActive(false);
    }
}
