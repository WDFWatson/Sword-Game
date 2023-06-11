using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private float angleSpeed = 5f;
    public Camera cam;
    
    public static InputController instance;
    
    
    public SwordController sword;
    
    [HideInInspector] public float currentAngle;
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

        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //New Method: Get a ray, and find where it intersects the plane that the sword rotates in
        Vector3 swordOrigin = sword.transform.position;
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 planeNormal = Vector3.back;
        float distanceToPlane = Vector3.Dot(planeNormal, swordOrigin - mouseRay.origin) /
                                Vector3.Dot(planeNormal, mouseRay.direction);
        Vector2 positionInPlane = mouseRay.GetPoint(distanceToPlane) - swordOrigin;
        
        float newAngle = Mathf.Rad2Deg * Mathf.Atan2(positionInPlane.y, positionInPlane.x) + 90;

        newAngle = ClampAngle(newAngle);

        float diff = newAngle - currentAngle;

        
        if(Mathf.Abs(diff) < angleSpeed)
        {
            currentAngle = newAngle;
        }
        else if ((diff > 0 && diff < 180) || diff < -180)
        {
            currentAngle += angleSpeed;
        }
        else
        {
            currentAngle -= angleSpeed;
        }
        

        currentAngle = ClampAngle(currentAngle);
    }

    private void FixedUpdate()
    {
        sword.SetRotation(currentAngle);
    }

    public static float ClampAngle(float angle)
    {
        if (angle < 0)
        {
            angle += 360;
        }
        else if (angle > 360)
        {
            angle -= 360;
        }

        return angle;
    }
}
