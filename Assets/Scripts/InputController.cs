using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputController : MonoBehaviour
{
    public static InputController instance;
    public float parryWindowLength = 0.5f;
    public float delayThreshold = 0.1f;
    
    public SwordController sword;
    
    [HideInInspector] public float angle;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    private Vector2 mousePos;
    [FormerlySerializedAs("timeSinceParry")] [HideInInspector] public float timeLeftParry;
    void Update()
    {
        mousePos.x = Input.mousePosition.x - (Screen.width * 0.5f);
        mousePos.y = Input.mousePosition.y - (Screen.height * 0.5f);
        angle = Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x) + 90;
        if (angle < 0)
        {
            angle += 360;
        }
        else if (angle > 360)
        {
            angle -= 360;
        }

        /*if (Input.GetButtonDown("Fire1"))
        {
            if (timeLeftParry < delayThreshold)
            {
                EffectManager.instance.Parry();
                timeLeftParry = parryWindowLength;
            }
        }*/
    }

    private void FixedUpdate()
    {
        sword.SetRotation(angle);
        /*if (timeLeftParry > 0)
        {
            timeLeftParry -= Time.fixedDeltaTime;
        }
        else
        {
            timeLeftParry = 0;
        }*/
    }
}
