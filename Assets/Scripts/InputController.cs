using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputController : MonoBehaviour
{
    public Camera cam;
    
    public static InputController instance;
    
    
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

        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Old method
        /*Vector2 mousePos;
        mousePos.x = Input.mousePosition.x - (Screen.width * 0.5f);
        mousePos.y = Input.mousePosition.y - (Screen.height * 0.5f);
        angle = Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x) + 90;*/
        //New Method: Get a ray, and find where it intersects the plane that the sword rotates in
        Vector3 swordOrigin = sword.transform.position;
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 planeNormal = Vector3.back;
        float distanceToPlane = Vector3.Dot(planeNormal, swordOrigin - mouseRay.origin) /
                                Vector3.Dot(planeNormal, mouseRay.direction);
        Vector2 positionInPlane = mouseRay.GetPoint(distanceToPlane) - swordOrigin;
        
        angle = Mathf.Rad2Deg * Mathf.Atan2(positionInPlane.y, positionInPlane.x) + 90;
        if (angle < 0)
        {
            angle += 360;
        }
        else if (angle > 360)
        {
            angle -= 360;
        }
    }

    private void FixedUpdate()
    {
        sword.SetRotation(angle);
    }
}
