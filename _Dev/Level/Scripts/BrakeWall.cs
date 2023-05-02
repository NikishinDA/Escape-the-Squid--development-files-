using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeWall : MonoBehaviour, IObstacle
{
   [SerializeField] private Rigidbody[] fragments;
   [SerializeField] private float force = 20f;
   [SerializeField] private Vector2 pos = new Vector2(10, 1);
   [SerializeField] private float radius = 100f;
   public void ObstacleCrash()
   {
      StartCoroutine(PhysicsFix());
   }

   private IEnumerator LifeTime(float time)
   {
      for (float t = 0; t < time; t += Time.deltaTime)
      {
         yield return null;
      }

      foreach (var fragment in fragments)
      {
         Destroy(fragment.gameObject);
      }
   }

   private IEnumerator PhysicsFix()
   {
      yield return new WaitForFixedUpdate();
      yield return new WaitForEndOfFrame();
      yield return new WaitForFixedUpdate();
      foreach (var fragment in fragments)
      {
         fragment.useGravity = true;
         fragment.isKinematic = false;
         fragment.AddExplosionForce(force, transform.position + pos.x*Vector3.back + pos.y*Vector3.up, 
            radius, 0f, ForceMode.Impulse);
      }
      StartCoroutine(LifeTime(2f));
   }
}
