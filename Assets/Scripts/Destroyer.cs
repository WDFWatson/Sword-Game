using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SwordController>(out var sword))
        {
            Destroy(other.gameObject);
        }
        
    }
}
