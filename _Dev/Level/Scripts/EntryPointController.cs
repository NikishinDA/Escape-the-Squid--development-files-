using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EntryPointController : MonoBehaviour
{
    //[SerializeField] private GameObject minigameCamera;
   private FinisherController _finisherController;

   private void Awake()
   {
       _finisherController = transform.parent.GetComponent<FinisherController>();
   }

   private void OnTriggerEnter(Collider other)
    {
        _finisherController.SetCameraInPos(other.transform);
        EventManager.Broadcast(GameEventsHandler.PlayerOnEntryPointEvent);
    }

   public void PlayerSetInPos()
   {
       _finisherController.PlayerInPos();
   }
}
