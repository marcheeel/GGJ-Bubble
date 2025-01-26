using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFingers : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Controller.current.GetComponent<Fingers>().fingersUnlocked = true;
            
            transform.SetParent(other.transform);
            Destroy(this);
        }
    }
}
