using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Dock : MonoBehaviour
{
    [SerializeField] BoxCollider2D doorCollider;
    [SerializeField] Animator doorAnimator;
    
    [Space(10)]
    
    public bool doorOpen;
    [SerializeField] private bool somethingOnDock;
    [SerializeField] private GameObject objectOnDock;

    private void Update()
    {
        if (somethingOnDock == true)
        {
            doorAnimator.SetTrigger("Open");
            doorCollider.enabled = false;
            doorOpen = true;
        }
        else if (somethingOnDock == false)
        {
            doorAnimator.SetTrigger("Close");
            doorCollider.enabled = true;
            doorOpen = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            objectOnDock = other.gameObject;
            somethingOnDock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            objectOnDock = null;
            somethingOnDock = false;
        }
    }
}
