using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private LayerMask layerMask;

    [SerializeField]
    private ParticleSystem hitEffect;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,
            transform.forward, out hit, speed * Time.deltaTime, layerMask))
        {
            transform.Translate(Vector3.forward * hit.distance);
            
            switch (hit.collider.gameObject.layer)
            {
                case 3://enemy
                {
                    hit.collider.GetComponent<EnemyHitBox>().TakeDamage();
                    Instantiate(hitEffect, hit.point, Quaternion.identity);
                }
                    break;
                default:
                {
                }
                    break;
            }
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
    }

    private IEnumerator LifeTime(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
