using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTracker : MonoBehaviour
{
   [SerializeField] private int numberHits;
   [SerializeField] private float limpTime;
   private CameraController _cameraController;
   private void Awake()
   {
      _cameraController = GetComponent<CameraController>();
      EventManager.AddListener<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
   }

   private void OnDestroy()
   {
      EventManager.RemoveListener<PlayerTakeDamageEvent>(OnPlayerTakeDamage);

   }

   private void OnPlayerTakeDamage(PlayerTakeDamageEvent obj)
   {
      numberHits -= obj.Damage;
      if (numberHits <= 0)
      {
         EventManager.Broadcast(GameEventsHandler.PlayerDeathEvent);
         Taptic.Failure();
         StopAllCoroutines();
      }
      else
      {
         StartCoroutine(LimpTime(limpTime));
         Taptic.Heavy();
      }
   }

   private IEnumerator LimpTime(float time)
   {
      for (float t = 0; t < time; t += Time.deltaTime)
      {
         yield return null;
      }
      _cameraController.ResetNoise();
      numberHits++;
   }
}
