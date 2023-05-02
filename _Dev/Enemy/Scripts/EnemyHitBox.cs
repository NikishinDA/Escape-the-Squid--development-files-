using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private int numHit;
    [SerializeField] private GameObject hurtBox;
    private EnemyController _controller;

    private void Awake()
    {
        _controller = transform.parent.GetComponent<EnemyController>();
    }

    public void TakeDamage()
    {
        numHit--;
        if (numHit <= 0)
        {
            hurtBox.SetActive(false);
            gameObject.layer = 0;
            _controller.EnemyDeath();
            StartCoroutine(DisableHitbox());
        }
    }

    private IEnumerator DisableHitbox()
    {
        yield return new WaitForFixedUpdate();
        gameObject.SetActive(false);
    }
}
