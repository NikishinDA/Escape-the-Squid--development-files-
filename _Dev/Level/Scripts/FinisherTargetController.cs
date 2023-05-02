using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherTargetController : MonoBehaviour
{

    [SerializeField] private float movementScale;
    [SerializeField] private float movementConstraint;
    [SerializeField] private float touchDeltaScale;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float newPosX = Input.GetAxis("Mouse X");
            if (Input.touchCount > 0)
            {
                newPosX = (Input.touches[0].deltaPosition.x / Screen.width) * touchDeltaScale;
            }

            newPosX *= movementScale * Time.deltaTime;
            if (!( Math.Abs(transform.localPosition.x + newPosX)  > movementConstraint))
            {
                transform.Translate(newPosX , 0, 0);
            }
        }
    }
}
