using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float angle;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        SetRotation(angle);
    }

    // Update is called once per frame

    public void SetRotation(float newAngle)
    {
        angle = newAngle;
        if (angle < 0)
        {
            angle += 360;
        }
        else if (angle > 360)
        {
            angle -= 360;
        }
        
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.forward,angle);
    }
}
