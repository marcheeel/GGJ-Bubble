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
    
    [SerializeField] private bool unlockedWithCoins;
    public int coinsNeeded;
    public bool doorOpen;
    [SerializeField] private bool somethingOnDock;
    [SerializeField] private GameObject objectOnDock;

    private void Update()
    {
        // Con monedas
        if (unlockedWithCoins)
        {
            switch (somethingOnDock)
            {
                case true when Controller.current.money >= coinsNeeded && unlockedWithCoins:
                    doorAnimator.SetTrigger("Open");
                    Controller.current.money -= coinsNeeded;
                    doorCollider.enabled = false;
                    doorOpen = true;
                    break;
                
                case true when Controller.current.money < coinsNeeded:
                    doorOpen = false;
                    break;
                
                case false:
                    doorAnimator.SetTrigger("Close");
                    doorCollider.enabled = true;
                    doorOpen = false;
                    break;
            }
        }
        else if (unlockedWithCoins == false)
        {
            switch (somethingOnDock)
            {
                case true:
                    doorAnimator.SetTrigger("Open");
                    Controller.current.money -= coinsNeeded;
                    doorCollider.enabled = false;
                    doorOpen = true;
                    break;
                case false:
                    doorAnimator.SetTrigger("Close");
                    doorCollider.enabled = true;
                    doorOpen = false;
                    break;
            }
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
