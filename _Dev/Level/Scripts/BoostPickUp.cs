using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Taptic.Success();
        EventManager.Broadcast(GameEventsHandler.BoostPickUpEvent);
        transform.parent.gameObject.SetActive(false);
    }
}
