using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFingers : MonoBehaviour
{
    [SerializeField] private bool tutorial;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Controller.current.GetComponent<Fingers>().fingersUnlocked = true;
            
            transform.SetParent(other.transform);
            if (tutorial)
            {
                Destroy(this);
            }
            else if (tutorial == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
