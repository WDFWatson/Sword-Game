using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    public float threshold = 10f;
    private void OnTriggerEnter(Collider other)
    {
        var sword = other.gameObject.GetComponent<SwordController>();
        float angleDiff = InputController.instance.angle - sword.angle;

        angleDiff = ClampAngleDiff(angleDiff);
        
        if (angleDiff >= 90 - threshold)
        {
            EffectManager.instance.Clash();
        }
        else
        {
            EffectManager.instance.Damage();
        }

        Destroy(sword.gameObject);
    }

    public static float ClampAngleDiff(float angleDiff)
    {
        if (angleDiff < 0)
        {
            angleDiff += 360;
        }
        else if (angleDiff > 360)
        {
            angleDiff -= 360;
        }

        if (angleDiff >= 90)
        {
            float t = 0;
            if (angleDiff >= 270)
            {
                t = Mathf.InverseLerp(360, 270, angleDiff);
            }
            else if (angleDiff >= 180)
            {
                t = Mathf.InverseLerp(180, 270, angleDiff);
            }
            else
            {
                t = Mathf.InverseLerp(180, 90, angleDiff);
            }

            angleDiff = Mathf.Lerp(0, 90, t);
        }

        return angleDiff;
    }
}
